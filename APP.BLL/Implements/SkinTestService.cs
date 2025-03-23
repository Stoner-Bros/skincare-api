using APP.BLL.Interfaces;
using APP.BLL.UOW;
using APP.Entity.DTOs.Request;
using APP.Entity.DTOs.Response;
using APP.Entity.Entities;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APP.BLL.Implements
{
    public class SkinTestService : ISkinTestService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SkinTestService> _logger;

        public SkinTestService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<SkinTestService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<SkinTestResponse>> GetAllAsync()
        {
            var skinTests = await _unitOfWork.SkinTests.GetQueryable()
                .Include(st => st.SkinTestQuestions)
                .ToListAsync();
            return _mapper.Map<IEnumerable<SkinTestResponse>>(skinTests);
        }

        public async Task<SkinTestResponse?> GetByIDAsync(int id)
        {
            var skinTest = await _unitOfWork.SkinTests.GetQueryable()
                .Include(st => st.SkinTestQuestions)
                .FirstOrDefaultAsync(st => st.SkinTestId == id);
            return skinTest == null ? null : _mapper.Map<SkinTestResponse>(skinTest);
        }

        public async Task<SkinTestResponse?> CreateAsync(SkinTestCreationRequest request)
        {
            var skinTest = _mapper.Map<SkinTest>(request);
            var createdSkinTest = await _unitOfWork.SkinTests.CreateAsync(skinTest);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<SkinTestResponse>(createdSkinTest);
        }

        public async Task<SkinTestResponse?> UpdateAsync(int id, SkinTestUpdationRequest request)
        {
            var skinTest = await _unitOfWork.SkinTests.GetByIDAsync(id);
            if (skinTest == null) return null;

            _mapper.Map(request, skinTest);
            skinTest.UpdatedAt = DateTime.Now;

            _unitOfWork.SkinTests.Update(skinTest);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<SkinTestResponse>(skinTest);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var skinTest = await _unitOfWork.SkinTests.GetByIDAsync(id);
            if (skinTest == null) return false;

            _unitOfWork.SkinTests.Delete(skinTest);
            return await _unitOfWork.SaveAsync() > 0;
        }
    }
}
