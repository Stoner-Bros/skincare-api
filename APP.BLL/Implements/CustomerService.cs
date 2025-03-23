using APP.BLL.Interfaces;
using APP.BLL.UOW;
using APP.Entity.DTOs.Request;
using APP.Entity.DTOs.Response;
using APP.Entity.Entities;
using APP.Utility;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.BLL.Implements
{
    public class CustomerService : ICustomerService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CustomerService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PaginationModel<CustomerResponse>> GetAllAsync(int pageNumber, int pageSize)
        {
            var query = _unitOfWork.Customers.GetQueryable()
                                    .Include(c => c.Account)
                                    .ThenInclude(a => a.AccountInfo);  // Truy vấn dữ liệu

            var totalRecords = await query.CountAsync(); // Tổng số bản ghi
            var accounts = await query
                .OrderBy(a => a.AccountId) // Sắp xếp (có thể thay đổi)
                .Skip((pageNumber - 1) * pageSize) // Bỏ qua các bản ghi trước đó
                .Take(pageSize) // Lấy số lượng bản ghi cần lấy
                .ToListAsync();

            return new PaginationModel<CustomerResponse>
            {
                Items = _mapper.Map<List<CustomerResponse>>(accounts),
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords
            };
        }

        public async Task<CustomerResponse?> GetByIDAsync(int id)
        {
            var customer = await _unitOfWork.Customers.GetDetailInfoByIDAsync(id);
            return customer == null ? null : _mapper.Map<CustomerResponse>(customer);
        }

        public async Task<CustomerResponse?> CreateAsync(AccountCreationRequest request)
        {
            var password = PasswordEncoder.Encode(request.Password);
            var account = new Account
            {
                Email = request.Email,
                Password = password,
                Role = "Customer"
            };
            account.AccountInfo = new AccountInfo { FullName = request.FullName };

            Customer customerCreated = null!;
            try
            {
                await _unitOfWork.SaveWithTransactionAsync(async () =>
                {
                    var accountCreated = await _unitOfWork.Accounts.CreateAsync(account);
                    await _unitOfWork.SaveAsync();

                    var customer = new Customer { AccountId = accountCreated.AccountId };
                    customerCreated = await _unitOfWork.Customers.CreateAsync(customer);
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the customer account at {Time}.", DateTime.Now);
                return null;
            }

            return _mapper.Map<CustomerResponse>(customerCreated);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var customer = await _unitOfWork.Customers.GetByIDAsync(id);
            if (customer == null) return false;
            _unitOfWork.Customers.Delete(customer);
            return await _unitOfWork.SaveAsync() > 0;
        }
    }
}
