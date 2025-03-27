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
    public class SkinTestResultService : ISkinTestResultService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SkinTestResultService> _logger;

        public SkinTestResultService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<SkinTestResultService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<SkinTestResultResponse>> GetAllAsync()
        {
            var results = await _unitOfWork.SkinTestResults.GetQueryable()
                .Include(r => r.SkinTestAnswer)
                .ToListAsync();

            return _mapper.Map<IEnumerable<SkinTestResultResponse>>(results);
        }

        public async Task<SkinTestResultResponse?> GetByResultIdAsync(int resultId)
        {
            var result = await _unitOfWork.SkinTestResults.GetByIDAsync(resultId);
            return result == null ? null : _mapper.Map<SkinTestResultResponse>(result);
        }

        public async Task<SkinTestResultResponse?> GetByAnswerIdAsync(int answerId)
        {
            var result = await _unitOfWork.SkinTestResults.GetQueryable().FirstOrDefaultAsync(s => s.SkinTestAnswerId == answerId);
            return result == null ? null : _mapper.Map<SkinTestResultResponse>(result);
        }

        public async Task<SkinTestResultResponse?> CreateBySkinTestAnswerIdAsync(int skinTestAnswerId, SkinTestResultRequest request)
        {
            // Check if a SkinTestResult already exists for the given SkinTestAnswerId
            var existingResult = await _unitOfWork.SkinTestResults.GetQueryable()
                .FirstOrDefaultAsync(r => r.SkinTestAnswerId == skinTestAnswerId);

            if (existingResult != null)
            {
                throw new InvalidOperationException("A result already exists for this Answer.");
            }

            var skinTestAnswer = await _unitOfWork.SkinTestAnswers.GetQueryable()
                .Include(a => a.Customer)
                .ThenInclude(c => c.Account)
                .Include(a => a.Guest)
                .FirstOrDefaultAsync(a => a.AnswerId == skinTestAnswerId);

            if (skinTestAnswer == null)
            {
                throw new InvalidOperationException("SkinTestAnswer not found.");
            }

            string email;
            if (skinTestAnswer.Customer != null)
            {
                email = skinTestAnswer.Customer.Account.Email;
            }
            else if (skinTestAnswer.Guest != null)
            {
                email = skinTestAnswer.Guest.Email;
            }
            else
            {
                throw new InvalidOperationException("Neither Customer nor Guest found for the SkinTestAnswer.");
            }

            var result = _mapper.Map<SkinTestResult>(request);
            result.SkinTestAnswerId = skinTestAnswerId;
            var createdResult = await _unitOfWork.SkinTestResults.CreateAsync(result);
            await _unitOfWork.SaveAsync();

            var response = _mapper.Map<SkinTestResultResponse>(createdResult);
            response.Email = email;
            return response;
        }

        public async Task<SkinTestResultResponse?> UpdateAsync(int resultId, SkinTestResultRequest request)
        {
            var result = await _unitOfWork.SkinTestResults.GetByIDAsync(resultId);
            if (result == null) return null;

            _mapper.Map(request, result);
            result.UpdatedAt = DateTime.Now;

            _unitOfWork.SkinTestResults.Update(result);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<SkinTestResultResponse>(result);
        }

        public async Task<bool> DeleteAsync(int resultId)
        {
            var result = await _unitOfWork.SkinTestResults.GetByIDAsync(resultId);
            if (result == null) return false;

            _unitOfWork.SkinTestResults.Delete(result);
            return await _unitOfWork.SaveAsync() > 0;
        }
    }
}
