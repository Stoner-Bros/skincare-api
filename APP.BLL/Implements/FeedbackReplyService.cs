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
    public class FeedbackReplyService : IFeedbackReplyService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<FeedbackReplyService> _logger;

        public FeedbackReplyService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<FeedbackReplyService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<FeedbackReplyResponse>> GetAllAsync()
        {
            var replies = await _unitOfWork.FeedbackReplies.GetQueryable()
                .Include(r => r.Staff)
                .ThenInclude(s => s.Account)
                .ThenInclude(a => a.AccountInfo)
                .ToListAsync();

            return _mapper.Map<IEnumerable<FeedbackReplyResponse>>(replies);
        }

        public async Task<IEnumerable<FeedbackReplyResponse>> GetByFeedbackIdAsync(int feedbackId)
        {
            var replies = await _unitOfWork.FeedbackReplies.GetQueryable()
                .Where(r => r.FeedbackId == feedbackId)
                .Include(r => r.Staff)
                .ThenInclude(s => s.Account)
                .ThenInclude(a => a.AccountInfo)
                .ToListAsync();

            return _mapper.Map<IEnumerable<FeedbackReplyResponse>>(replies);
        }

        public async Task<FeedbackReplyResponse?> CreateByFeedbackIdAsync(int feedbackId, FeedbackReplyCreationRequest request, int staffId)
        {
            var feedback = await _unitOfWork.Feedbacks.GetByIDAsync(feedbackId);
            if (feedback == null)
            {
                throw new InvalidOperationException("Feedback not found.");
            }

            var staff = await _unitOfWork.Staffs.GetQueryable()
                .Include(s => s.Account)
                .ThenInclude(a => a.AccountInfo)
                .FirstOrDefaultAsync(s => s.AccountId == staffId);
            if (staff == null)
            {
                throw new InvalidOperationException("Staff not found.");
            }

            var feedbackReply = _mapper.Map<FeedbackReply>(request);
            feedbackReply.FeedbackId = feedbackId;
            feedbackReply.StaffId = staffId;
            feedbackReply.CreatedAt = DateTime.Now;

            var createdReply = await _unitOfWork.FeedbackReplies.CreateAsync(feedbackReply);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<FeedbackReplyResponse>(createdReply);
        }

        public async Task<bool> UpdateAsync(int id, FeedbackReplyUpdationRequest request)
        {
            var feedbackReply = await _unitOfWork.FeedbackReplies.GetByIDAsync(id);
            if (feedbackReply == null) return false;

            _mapper.Map(request, feedbackReply);
            feedbackReply.UpdatedAt = DateTime.Now;

            _unitOfWork.FeedbackReplies.Update(feedbackReply);
            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var feedbackReply = await _unitOfWork.FeedbackReplies.GetByIDAsync(id);
            if (feedbackReply == null) return false;

            _unitOfWork.FeedbackReplies.Delete(feedbackReply);
            return await _unitOfWork.SaveAsync() > 0;
        }
    }
}

