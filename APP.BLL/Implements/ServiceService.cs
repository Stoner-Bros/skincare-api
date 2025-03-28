using APP.BLL.Interfaces;
using APP.BLL.UOW;
using APP.Entity.DTOs.Request;
using APP.Entity.DTOs.Response;
using APP.Entity.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace APP.BLL.Implements
{
    public class ServiceService : IServiceService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ServiceService> _logger;

        public ServiceService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ServiceService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PaginationModel<ServiceResponse>> GetAllAsync(int pageNumber, int pageSize)
        {
            var query = _unitOfWork.Services.GetQueryable()
                .Where(a => a.IsAvailable);  // Truy vấn dữ liệu

            var totalRecords = await query.CountAsync(); // Tổng số bản ghi
            var services = await query
                .OrderBy(a => a.ServiceId) // Sắp xếp (có thể thay đổi)
                .Skip((pageNumber - 1) * pageSize) // Bỏ qua các bản ghi trước đó
                .Take(pageSize) // Lấy số lượng bản ghi cần lấy
                .ToListAsync();

            return new PaginationModel<ServiceResponse>
            {
                Items = _mapper.Map<List<ServiceResponse>>(services),
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords
            };
        }

        public async Task<ServiceResponse?> GetByIDAsync(int id)
        {
            var service = await _unitOfWork.Services.GetByIDAsync(id);
            return service == null ? null : _mapper.Map<ServiceResponse>(service);
        }

        public async Task<ServiceResponse?> CreateAsync(ServiceCreationRequest request)
        {
            var service = _mapper.Map<Service>(request);
            var createdService = await _unitOfWork.Services.CreateAsync(service);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<ServiceResponse>(createdService);
        }

        public async Task<bool> UpdateAsync(int id, ServiceUpdationRequest request)
        {
            var service = await _unitOfWork.Services.GetByIDAsync(id);
            if (service == null) return false;
            _mapper.Map(request, service);
            _unitOfWork.Services.Update(service);
            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var service = await _unitOfWork.Services.GetByIDAsync(id);
            if (service == null) return false;
            //_unitOfWork.Services.Delete(service);
            service.IsAvailable = false;
            _unitOfWork.Services.Update(service);
            return await _unitOfWork.SaveAsync() > 0;
        }
    }
}
