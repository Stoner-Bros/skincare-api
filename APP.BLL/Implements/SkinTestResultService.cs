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
using System.Text.Json;
namespace APP.BLL.Implements
{
    public class SkinTestResultService : ISkinTestResultService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SkinTestResultService> _logger;
        private readonly IEmailService _emailService;

        public SkinTestResultService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<SkinTestResultService> logger, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _emailService = emailService;
        }

        public async Task<IEnumerable<SkinTestResultResponse>> GetAllAsync()
        {
            var results = await _unitOfWork.SkinTestResults.GetQueryable()
                .Include(r => r.SkinTestAnswer)
                .ThenInclude(a => a.Customer)
                .ThenInclude(c => c.Account)
                .Include(r => r.SkinTestAnswer.Guest)
                .ToListAsync();

            return results.Select(result =>
            {
                var response = _mapper.Map<SkinTestResultResponse>(result);
                response.Email = result.SkinTestAnswer.Customer != null ? result.SkinTestAnswer.Customer.Account.Email : result.SkinTestAnswer.Guest.Email;
                return response;
            }).ToList();
        }

        public async Task<SkinTestResultResponse?> GetByResultIdAsync(int resultId)
        {
            var result = await _unitOfWork.SkinTestResults.GetQueryable()
                .Include(r => r.SkinTestAnswer)
                .ThenInclude(a => a.Customer)
                .ThenInclude(c => c.Account)
                .Include(r => r.SkinTestAnswer.Guest)
                .FirstOrDefaultAsync(r => r.ResultId == resultId);

            if (result == null) return null;

            var response = _mapper.Map<SkinTestResultResponse>(result);
            response.Email = result.SkinTestAnswer.Customer != null ? result.SkinTestAnswer.Customer.Account.Email : result.SkinTestAnswer.Guest.Email;
            return response;
        }

        public async Task<SkinTestResultResponse?> GetByAnswerIdAsync(int answerId)
        {
            var result = await _unitOfWork.SkinTestResults.GetQueryable()
                .Include(r => r.SkinTestAnswer)
                .ThenInclude(a => a.Customer)
                .ThenInclude(c => c.Account)
                .Include(r => r.SkinTestAnswer.Guest)
                .FirstOrDefaultAsync(s => s.SkinTestAnswerId == answerId);

            if (result == null) return null;

            var response = _mapper.Map<SkinTestResultResponse>(result);
            response.Email = result.SkinTestAnswer.Customer != null ? result.SkinTestAnswer.Customer.Account.Email : result.SkinTestAnswer.Guest.Email;
            return response;
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
            string userName;
            if (skinTestAnswer.Customer != null)
            {
                email = skinTestAnswer.Customer.Account.Email;
                userName = skinTestAnswer.Customer.Account.AccountInfo.FullName;
            }
            else if (skinTestAnswer.Guest != null)
            {
                email = skinTestAnswer.Guest.Email;
                userName = skinTestAnswer.Guest.FullName;
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

            //var resultData = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(result.Result);

            //var placeholders = new Dictionary<string, string>
            //{
            //    { "Subject", "Your Skin Test Result" },
            //    { "UserName", userName },
            //    { "TreatmentName", resultData["treatmentName"].GetString() },
            //    { "Description", resultData["description"].GetString() },
            //    { "Duration", resultData["duration"].GetInt32().ToString() },
            //    { "Price", resultData["price"].GetInt32().ToString() },
            //    { "Message", resultData["message"].GetString() }
            //};
            //await _emailService.SendEmail(email, "SkinTestResultEmail", placeholders);
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
