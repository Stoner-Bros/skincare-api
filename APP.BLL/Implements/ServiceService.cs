using APP.BLL.Interfaces;
using APP.BLL.UOW;
using APP.Entity.DTOs.Request;
using APP.Entity.DTOs.Response;
using APP.Entity.Entities;
using AutoMapper;
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

        public async Task<IEnumerable<ServiceResponse>> GetAllAsync()
        {
            var services = await _unitOfWork.Services.GetAllAsync();
            return _mapper.Map<IEnumerable<ServiceResponse>>(services);
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
            _unitOfWork.Services.Delete(service);
            return await _unitOfWork.SaveAsync() > 0;
        }
    }
}
