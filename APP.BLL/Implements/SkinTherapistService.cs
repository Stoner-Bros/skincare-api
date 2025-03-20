using APP.BLL.Interfaces;
using APP.BLL.UOW;
using APP.Entity.DTOs.Request;
using APP.Entity.DTOs.Response;
using APP.Entity.Entities;
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
    public class SkinTherapistService : ISkinTherapistService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SkinTherapistService> _logger;

        public SkinTherapistService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<SkinTherapistService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PaginationModel<SkinTherapistResponse>> GetAllAsync(int pageNumber, int pageSize)
        {
            var query = _unitOfWork.SkinTherapists.GetQueryable();  // Truy vấn dữ liệu

            var totalRecords = await query.CountAsync(); // Tổng số bản ghi
            var accounts = await query
                .OrderBy(a => a.AccountId) // Sắp xếp (có thể thay đổi)
                .Skip((pageNumber - 1) * pageSize) // Bỏ qua các bản ghi trước đó
                .Take(pageSize) // Lấy số lượng bản ghi cần lấy
                .ToListAsync();

            return new PaginationModel<SkinTherapistResponse>
            {
                Items = _mapper.Map<List<SkinTherapistResponse>>(accounts),
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords
            };
        }

        public async Task<SkinTherapistResponse?> GetByIDAsync(int id)
        {
            var therapist = await _unitOfWork.SkinTherapists.GetDetailInfoByIDAsync(id);
            return therapist == null ? null : _mapper.Map<SkinTherapistResponse>(therapist);
        }

        public async Task<SkinTherapistResponse?> CreateAsync(SkinTherapistCreationRequest request)
        {
            var therapist = _mapper.Map<SkinTherapist>(request);
            var createdTherapist = await _unitOfWork.SkinTherapists.CreateAsync(therapist);
            await _unitOfWork.SaveAsync();
            var account = await _unitOfWork.Accounts.GetByIDAsync(therapist.AccountId);
            if (account == null)
            {
                return null;
            }
            account.Role = "Skin Therapist";
            await _unitOfWork.SaveAsync();
            return _mapper.Map<SkinTherapistResponse>(createdTherapist);
        }

        public async Task<bool> UpdateAsync(int id, SkinTherapistUpdationRequest request)
        {
            var therapist = await _unitOfWork.SkinTherapists.GetByIDAsync(id);
            if (therapist == null) return false;
            _mapper.Map(request, therapist);
            _unitOfWork.SkinTherapists.Update(therapist);
            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var therapist = await _unitOfWork.SkinTherapists.GetByIDAsync(id);
            if (therapist == null) return false;
            _unitOfWork.SkinTherapists.Delete(therapist);
            return await _unitOfWork.SaveAsync() > 0;
        }
    }
}
