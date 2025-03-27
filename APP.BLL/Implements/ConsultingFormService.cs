using APP.BLL.Interfaces;
using APP.BLL.UOW;
using APP.Entity.DTOs.Request;
using APP.Entity.DTOs.Response;
using APP.Entity.Entities;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APP.BLL.Implements
{
    public class ConsultingFormService : IConsultingFormService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ConsultingFormService> _logger;

        public ConsultingFormService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ConsultingFormService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PaginationModel<object>> GetAllAsync(int pageNumber, int pageSize)
        {
            var query = _unitOfWork.ConsultingForms.GetQueryable()
                         .AsNoTracking()
                         .Select(f => new
                         {
                             f.ConsultingFormId,
                             f.Message,
                             f.Status,
                             f.CreatedAt,
                             f.UpdatedAt,

                             Staff = f.StaffId != null ? new
                             {
                                 f.Staff.AccountId,
                                 f.Staff.Account.AccountInfo.FullName,
                             } : null,

                             Customer = f.CustomerId != null ? new
                             {
                                 f.Customer.AccountId,
                                 f.Customer.Account.AccountInfo.FullName,
                                 f.Customer.Account.AccountInfo.Phone,
                             } : null,

                             Guest = f.GuestId != null ? new
                             {
                                 f.Guest.GuestId,
                                 f.Guest.FullName,
                                 f.Guest.Phone,
                             } : null,
                         });

            var totalRecords = await query.CountAsync();
            var forms = await query
                .OrderBy(a => a.ConsultingFormId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginationModel<object>
            {
                Items = forms,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords
            };
        }

        public async Task<object?> GetByIDAsync(int id)
        {
            var form = _unitOfWork.ConsultingForms.GetQueryable()
                         .AsNoTracking()
                         .Where(a => a.ConsultingFormId == id)
                         .Select(f => new
                         {
                             f.ConsultingFormId,
                             f.Message,
                             f.Status,
                             f.CreatedAt,
                             f.UpdatedAt,

                             Staff = f.StaffId != null ? new
                             {
                                 f.Staff.AccountId,
                                 f.Staff.Account.AccountInfo.FullName,
                             } : null,

                             Customer = f.CustomerId != null ? new
                             {
                                 f.Customer.AccountId,
                                 f.Customer.Account.AccountInfo.FullName,
                                 f.Customer.Account.AccountInfo.Phone,
                             } : null,

                             Guest = f.GuestId != null ? new
                             {
                                 f.Guest.GuestId,
                                 f.Guest.FullName,
                                 f.Guest.Phone,
                             } : null,
                         });
            return form.Any() ? form.ElementAt(0) : null;
        }

        public async Task<bool> CreateAsync(ConsultingFormCreationRequest request)
        {
            var customerAccount = await _unitOfWork.Accounts.GetByEmailAsync(request.Email);
            if (customerAccount != null && customerAccount.Role != "Customer")
            {
                throw new Exception("Email exists in another role.");
            }

            var result = await _unitOfWork.SaveWithTransactionAsync(async () =>
            {
                var consultingForm = _mapper.Map<ConsultingForm>(request);
                consultingForm.Status = "Pending"; // Set default status

                if (customerAccount == null)
                {
                    var existingGuest = await _unitOfWork.Guests.GetByEmailAsync(request.Email);
                    if (existingGuest == null)
                    {
                        var guest = new Guest
                        {
                            Email = request.Email,
                            Phone = request.Phone,
                            FullName = request.FullName,
                        };
                        var createdGuest = await _unitOfWork.Guests.CreateAsync(guest);
                        await _unitOfWork.SaveAsync();
                        consultingForm.GuestId = createdGuest.GuestId;
                    }
                    else
                    {
                        consultingForm.GuestId = existingGuest.GuestId;
                    }
                }
                else
                {
                    consultingForm.CustomerId = customerAccount.AccountId;
                }

                var createdForm = await _unitOfWork.ConsultingForms.CreateAsync(consultingForm);
            }) > 0;

            return result;
        }

        public async Task<bool> UpdateAsync(int id, ConsultingFormUpdationRequest request)
        {
            var form = await _unitOfWork.ConsultingForms.GetByIDAsync(id);
            if (form == null) return false;
            _mapper.Map(request, form);
            form.UpdatedAt = DateTime.Now;
            _unitOfWork.ConsultingForms.Update(form);
            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var form = await _unitOfWork.ConsultingForms.GetByIDAsync(id);
            if (form == null) return false;
            //_unitOfWork.ConsultingForms.Delete(form);
            form.Status = "Cancelled";
            _unitOfWork.ConsultingForms.Update(form);
            return await _unitOfWork.SaveAsync() > 0;
        }
    }
}
