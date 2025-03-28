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
    public class TreatmentService : ITreatmentService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<TreatmentService> _logger;

        public TreatmentService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<TreatmentService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PaginationModel<TreatmentResponse>> GetAllAsync(int serviceId, int pageNumber, int pageSize)
        {
            var query = _unitOfWork.Treatments.GetQueryable()
                            .Where(t => t.ServiceId == serviceId && t.IsAvailable);  // Truy vấn dữ liệu

            var totalRecords = await query.CountAsync(); // Tổng số bản ghi
            var treatments = await query
                .OrderBy(a => a.TreatmentId) // Sắp xếp (có thể thay đổi)
                .Skip((pageNumber - 1) * pageSize) // Bỏ qua các bản ghi trước đó
                .Take(pageSize) // Lấy số lượng bản ghi cần lấy
                .ToListAsync();

            return new PaginationModel<TreatmentResponse>
            {
                Items = _mapper.Map<List<TreatmentResponse>>(treatments),
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords
            };
        }

        public async Task<TreatmentResponse?> GetByIDAsync(int id)
        {
            var treatment = await _unitOfWork.Treatments.GetByIDAsync(id);
            return treatment == null ? null : _mapper.Map<TreatmentResponse>(treatment);
        }

        public async Task<TreatmentResponse?> CreateAsync(TreatmentRequest request)
        {
            var treatment = _mapper.Map<Treatment>(request);
            var createdTreatment = await _unitOfWork.Treatments.CreateAsync(treatment);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<TreatmentResponse>(createdTreatment);
        }

        public async Task<bool> UpdateAsync(int id, TreatmentRequest request)
        {
            var treatment = await _unitOfWork.Treatments.GetByIDAsync(id);
            if (treatment == null) return false;
            _mapper.Map(request, treatment);
            _unitOfWork.Treatments.Update(treatment);
            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var treatment = await _unitOfWork.Treatments.GetByIDAsync(id);
            if (treatment == null) return false;
            //_unitOfWork.Treatments.Delete(treatment);
            treatment.IsAvailable = false;
            _unitOfWork.Treatments.Update(treatment);
            return await _unitOfWork.SaveAsync() > 0;
        }
    }
}
