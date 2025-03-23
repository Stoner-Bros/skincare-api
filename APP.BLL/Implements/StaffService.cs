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
    public class StaffService : IStaffService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<StaffService> _logger;

        public StaffService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<StaffService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PaginationModel<StaffResponse>> GetAllAsync(int pageNumber, int pageSize)
        {
            var query = _unitOfWork.Staffs.GetQueryable()
                                    .Include(s => s.Account)
                                    .ThenInclude(a => a.AccountInfo);  // Truy vấn dữ liệu

            var totalRecords = await query.CountAsync(); // Tổng số bản ghi
            var accounts = await query
                .OrderBy(a => a.AccountId) // Sắp xếp (có thể thay đổi)
                .Skip((pageNumber - 1) * pageSize) // Bỏ qua các bản ghi trước đó
                .Take(pageSize) // Lấy số lượng bản ghi cần lấy
                .ToListAsync();

            return new PaginationModel<StaffResponse>
            {
                Items = _mapper.Map<List<StaffResponse>>(accounts),
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords
            };
        }

        public async Task<StaffResponse?> GetByIDAsync(int id)
        {
            var staff = await _unitOfWork.Staffs.GetDetailInfoByIDAsync(id);
            return staff == null ? null : _mapper.Map<StaffResponse>(staff);
        }

        public async Task<StaffResponse?> CreateAsync(StaffCreationRequest request)
        {
            var password = PasswordEncoder.Encode(request.Password);
            var account = new Account
            {
                Email = request.Email,
                Password = password,
                Role = "Staff"
            };
            account.AccountInfo = new AccountInfo { FullName = request.FullName };

            Staff staffCreated = null!;
            try
            {
                await _unitOfWork.SaveWithTransactionAsync(async () =>
                {
                    var accountCreated = await _unitOfWork.Accounts.CreateAsync(account);
                    await _unitOfWork.SaveAsync();

                    var staff = _mapper.Map<Staff>(request);
                    staff.AccountId = accountCreated.AccountId;
                    staffCreated = await _unitOfWork.Staffs.CreateAsync(staff);
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the staff account at {Time}.", DateTime.Now);
                return null;
            }

            return _mapper.Map<StaffResponse>(staffCreated);
        }

        public async Task<bool> UpdateAsync(int id, StaffUpdationRequest request)
        {
            var staff = await _unitOfWork.Staffs.GetDetailInfoByIDAsync(id);
            if (staff == null) return false;
            var accountUpdateInfo = new AccountUpdationRequest
            {
                Address = request.Address,
                Avatar = request.Avatar,
                Dob = request.Dob,
                FullName = request.FullName,
                OtherInfo = request.OtherInfo,
                Phone = request.Phone,
            };

            _mapper.Map(request, staff);
            _mapper.Map(accountUpdateInfo, staff.Account.AccountInfo);
            staff.Account.UpdateAt = DateTime.Now;

            _unitOfWork.Staffs.Update(staff);
            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var staff = await _unitOfWork.Staffs.GetByIDAsync(id);
            if (staff == null) return false;
            _unitOfWork.Staffs.Delete(staff);
            return await _unitOfWork.SaveAsync() > 0;
        }
    }
}
