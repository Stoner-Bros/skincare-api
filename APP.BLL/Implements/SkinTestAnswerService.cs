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
    public class SkinTestAnswerService : ISkinTestAnswerService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SkinTestAnswerService> _logger;

        public SkinTestAnswerService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<SkinTestAnswerService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<SkinTestAnswerResponse>> GetAllAsync()
        {
            var answers = await _unitOfWork.SkinTestAnswers.GetQueryable()
                .Include(a => a.SkinTest)
                .ThenInclude(st => st.SkinTestQuestions)
                .Include(a => a.Customer)
                .Include(a => a.Guest)
                .ToListAsync();

            return _mapper.Map<IEnumerable<SkinTestAnswerResponse>>(answers);
        }

        public async Task<SkinTestAnswerResponse?> CreateSkinTestAnswerAsync(SkinTestAnswerRequest request)
        {
            // Retrieve the SkinTest to get the number of questions
            var skinTest = await _unitOfWork.SkinTests.GetQueryable()
                .Include(st => st.SkinTestQuestions)
                .FirstOrDefaultAsync(st => st.SkinTestId == request.SkinTestId);

            if (skinTest == null)
            {
                throw new KeyNotFoundException("SkinTest not found.");
            }

            // Validate the number of answers
            if (request.Answers.Length > skinTest.SkinTestQuestions.Count)
            {
                throw new InvalidOperationException("The number of answers cannot be greater than the number of questions.");
            }

            var answer = _mapper.Map<SkinTestAnswer>(request);
            var createdAnswer = await _unitOfWork.SkinTestAnswers.CreateAsync(answer);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<SkinTestAnswerResponse>(createdAnswer);
        }
    }
}
