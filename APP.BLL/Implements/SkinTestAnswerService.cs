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
                .ThenInclude(c => c.Account)
                .ThenInclude(a => a.AccountInfo)
                .Include(a => a.Guest)
                .ToListAsync();

            return _mapper.Map<IEnumerable<SkinTestAnswerResponse>>(answers);
        }

        public async Task<SkinTestAnswerResponse?> GetByIdAsync(int id)
        {
            var answer = await _unitOfWork.SkinTestAnswers.GetQueryable()
                .Include(a => a.SkinTest)
                .ThenInclude(st => st.SkinTestQuestions)
                .Include(a => a.Customer)
                .ThenInclude(c => c.Account)
                .ThenInclude(a => a.AccountInfo)
                .Include(a => a.Guest)
                .FirstOrDefaultAsync(a => a.AnswerId == id);

            return answer == null ? null : _mapper.Map<SkinTestAnswerResponse>(answer);
        }

        public async Task<IEnumerable<SkinTestAnswerResponse>> GetByCustomerId(int customerId)
        {
            var answer = await _unitOfWork.SkinTestAnswers.GetQueryable()
                .Where(a => a.CustomerId == customerId)
                .Include(a => a.SkinTest)
                .ThenInclude(st => st.SkinTestQuestions)
                .Include(a => a.Customer)
                .ThenInclude(c => c.Account)
                .ThenInclude(a => a.AccountInfo)
                .Include(a => a.Guest)
                .ToListAsync();
            return _mapper.Map<IEnumerable<SkinTestAnswerResponse>>(answer);
        }

        public async Task<SkinTestAnswerResponse?> CreateSkinTestAnswerAsync(SkinTestAnswerRequest request)
        {
            // Check if the email belongs to a customer
            var customerAccount = await _unitOfWork.Accounts.GetByEmailAsync(request.Email);
            if (customerAccount != null && customerAccount.Role != "Customer")
            {
                throw new InvalidOperationException("Email exists in another role.");
            }

            var skinTestAnswer = _mapper.Map<SkinTestAnswer>(request);

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
                    skinTestAnswer.GuestId = createdGuest.GuestId;
                }
                else
                {
                    skinTestAnswer.GuestId = existingGuest.GuestId;
                }
            }
            else
            {
                skinTestAnswer.CustomerId = customerAccount.AccountId;
                var customer = await _unitOfWork.Customers.GetQueryable()
                    .Include(c => c.Account)
                    .ThenInclude(a => a.AccountInfo)
                    .FirstOrDefaultAsync(c => c.AccountId == customerAccount.AccountId);
                if (customer != null)
                {
                    skinTestAnswer.Customer = customer;
                }

            }


            var skinTest = await _unitOfWork.SkinTests.GetQueryable()
                .Include(st => st.SkinTestQuestions)
                .FirstOrDefaultAsync(st => st.SkinTestId == request.SkinTestId);
            if (skinTest == null)
            {
                throw new InvalidOperationException("SkinTest not found.");
            }
            skinTestAnswer.SkinTest = skinTest;

            var createdAnswer = await _unitOfWork.SkinTestAnswers.CreateAsync(skinTestAnswer);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<SkinTestAnswerResponse>(createdAnswer);
        }

        public async Task<bool> DeleteSkinTestAnswerAsync(int id)
        {
            var answer = await _unitOfWork.SkinTestAnswers.GetByIDAsync(id);
            if (answer == null) return false;
            _unitOfWork.SkinTestAnswers.Delete(answer);
            return await _unitOfWork.SaveAsync() > 0;
        }
    }
}

