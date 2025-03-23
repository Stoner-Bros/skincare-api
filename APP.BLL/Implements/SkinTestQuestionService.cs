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
    public class SkinTestQuestionService : ISkinTestQuestionService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SkinTestQuestionService> _logger;

        public SkinTestQuestionService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<SkinTestQuestionService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<SkinTestQuestionResponse>> GetAllAsync()
        {
            var questions = await _unitOfWork.SkinTestQuestions.GetAllAsync();
            return _mapper.Map<IEnumerable<SkinTestQuestionResponse>>(questions);
        }

        public async Task<SkinTestQuestionResponse?> GetByIDAsync(int id)
        {
            var question = await _unitOfWork.SkinTestQuestions.GetByIDAsync(id);
            return question == null ? null : _mapper.Map<SkinTestQuestionResponse>(question);
        }

        public async Task<SkinTestQuestionResponse?> CreateAsync(int skinTestId, SkinTestQuestionCreationRequest request)
        {
            var question = _mapper.Map<SkinTestQuestion>(request);
            question.SkinTestId = skinTestId;
            var createdQuestion = await _unitOfWork.SkinTestQuestions.CreateAsync(question);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<SkinTestQuestionResponse>(createdQuestion);
        }

        public async Task<SkinTestQuestionResponse?> UpdateAsync(int id, SkinTestQuestionUpdationRequest request)
        {
            var question = await _unitOfWork.SkinTestQuestions.GetByIDAsync(id);
            if (question == null) return null;

            _mapper.Map(request, question);
            question.UpdatedAt = DateTime.Now;

            _unitOfWork.SkinTestQuestions.Update(question);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<SkinTestQuestionResponse>(question);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var question = await _unitOfWork.SkinTestQuestions.GetByIDAsync(id);
            if (question == null) return false;

            _unitOfWork.SkinTestQuestions.Delete(question);
            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<IEnumerable<SkinTestQuestionResponse>> GetBySkinTestIdAsync(int skinTestId)
        {
            var questions = await _unitOfWork.SkinTestQuestions.GetQueryable()
                .Where(q => q.SkinTestId == skinTestId)
                .ToListAsync();
            return _mapper.Map<IEnumerable<SkinTestQuestionResponse>>(questions);
        }
    }
}
