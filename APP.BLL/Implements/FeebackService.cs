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
    public class FeedbackService : IFeedbackService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<FeedbackService> _logger;

        public FeedbackService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<FeedbackService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PaginationModel<FeedbackResponse>> GetAllAsync(int pageNumber, int pageSize)
        {
            var query = _unitOfWork.Feedbacks.GetQueryable()
                         .AsNoTracking()
                         .Include(f => f.Booking)
                         .ThenInclude(b => b.Customer)
                         .ThenInclude(c => c.Account)
                         .ThenInclude(a => a.AccountInfo)
                         .Include(f => f.Booking)
                         .ThenInclude(b => b.Guest)
                         .Include(f => f.FeedbackReplies)
                         .Select(f => new FeedbackResponse
                         {
                             FeedbackId = f.FeedbackId,
                             BookingId = f.BookingId,
                             FeedbackBy = f.Booking.Customer != null ? f.Booking.Customer.Account.AccountInfo.FullName : f.Booking.Guest.FullName,
                             Rating = f.Rating,
                             Comment = f.Comment,
                             CreatedAt = f.CreatedAt,
                             UpdatedAt = f.UpdatedAt,
                             FeedbackReplies = f.FeedbackReplies.Select(r => new FeedbackReplyResponse
                             {
                                 FeedbackReplyId = r.FeedbackReplyId,
                                 FeedbackId = r.FeedbackId,
                                 StaffName = r.Staff.Account.AccountInfo.FullName,
                                 Reply = r.Reply,
                                 CreatedAt = r.CreatedAt,
                                 UpdatedAt = r.UpdatedAt
                             }).ToList()
                         });

            var totalRecords = await query.CountAsync();
            var feedbacks = await query
                .OrderBy(f => f.FeedbackId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginationModel<FeedbackResponse>
            {
                Items = feedbacks,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords
            };
        }

        public async Task<FeedbackResponse?> GetByIDAsync(int id)
        {
            var feedback = await _unitOfWork.Feedbacks.GetQueryable()
                .AsNoTracking()
                .Include(f => f.Booking)
                .ThenInclude(b => b.Customer)
                .ThenInclude(c => c.Account)
                .ThenInclude(a => a.AccountInfo)
                .Include(f => f.Booking)
                .ThenInclude(b => b.Guest)
                .Include(f => f.FeedbackReplies)
                .ThenInclude(r => r.Staff)
                .ThenInclude(s => s.Account)
                .ThenInclude(a => a.AccountInfo)
                .FirstOrDefaultAsync(f => f.FeedbackId == id);

            if (feedback == null) return null;

            var feedbackResponse = _mapper.Map<FeedbackResponse>(feedback);
            feedbackResponse.FeedbackBy = feedback.Booking.Customer != null ? feedback.Booking.Customer.Account.AccountInfo.FullName : feedback.Booking.Guest.FullName;
            var feedbackReplies = feedback.FeedbackReplies.Select(r => new FeedbackReplyResponse
            {
                FeedbackReplyId = r.FeedbackReplyId,
                FeedbackId = r.FeedbackId,
                StaffName = r.Staff.Account.AccountInfo.FullName,
                Reply = r.Reply,
                CreatedAt = r.CreatedAt,
                UpdatedAt = r.UpdatedAt
            }).ToList();
            feedbackResponse.FeedbackReplies = feedbackReplies;

            return feedbackResponse;
        }

        public async Task<FeedbackResponse?> GetByBookingIdAsync(int bookingId)
        {
            var feedback = await _unitOfWork.Feedbacks.GetQueryable()
                .AsNoTracking()
                .Include(f => f.Booking)
                .ThenInclude(b => b.Customer)
                .ThenInclude(c => c.Account)
                .ThenInclude(a => a.AccountInfo)
                .Include(f => f.Booking)
                .ThenInclude(b => b.Guest)
                .Include(f => f.FeedbackReplies)
                .ThenInclude(r => r.Staff)
                .ThenInclude(s => s.Account)
                .ThenInclude(a => a.AccountInfo)
                .FirstOrDefaultAsync(f => f.BookingId == bookingId);

            if (feedback == null) return null;

            var feedbackResponse = _mapper.Map<FeedbackResponse>(feedback);
            feedbackResponse.FeedbackBy = feedback.Booking.Customer != null ? feedback.Booking.Customer.Account.AccountInfo.FullName : feedback.Booking.Guest.FullName;
            var feedbackReplies = feedback.FeedbackReplies.Select(r => new FeedbackReplyResponse
            {
                FeedbackReplyId = r.FeedbackReplyId,
                FeedbackId = r.FeedbackId,
                StaffName = r.Staff.Account.AccountInfo.FullName,
                Reply = r.Reply,
                CreatedAt = r.CreatedAt,
                UpdatedAt = r.UpdatedAt
            }).ToList();
            feedbackResponse.FeedbackReplies = feedbackReplies;

            return feedbackResponse;
        }

        public async Task<FeedbackResponse?> CreateAsync(int bookingId, FeedbackCreationRequest request, int accountId)
        {
            // Check if feedback already exists for the given booking
            var existingFeedback = await _unitOfWork.Feedbacks.GetQueryable()
                .FirstOrDefaultAsync(f => f.BookingId == bookingId);

            if (existingFeedback != null)
            {
                throw new InvalidOperationException("Feedback already exists for this booking.");
            }

            var booking = await _unitOfWork.Bookings.GetQueryable()
                .Include(b => b.Customer)
                .ThenInclude(c => c.Account)
                .ThenInclude(a => a.AccountInfo)
                .Include(b => b.Guest)
                .FirstOrDefaultAsync(b => b.BookingId == bookingId);

            if (booking == null)
            {
                throw new InvalidOperationException("Booking not found.");
            }

            // Check if the account creating the feedback is the same as the account associated with the booking
            if (booking.Customer != null && booking.Customer.AccountId != accountId)
            {
                throw new InvalidOperationException("You are not authorized to create feedback for this booking.");
            }

            var feedback = _mapper.Map<Feedback>(request);
            feedback.BookingId = bookingId;
            feedback.CreatedAt = DateTime.Now;

            var createdFeedback = await _unitOfWork.Feedbacks.CreateAsync(feedback);
            await _unitOfWork.SaveAsync();

            var feedbackResponse = _mapper.Map<FeedbackResponse>(createdFeedback);
            feedbackResponse.FeedbackBy = booking.Customer != null ? booking.Customer.Account.AccountInfo.FullName : booking.Guest.FullName;

            return feedbackResponse;
        }

        public async Task<bool> UpdateAsync(int id, FeedbackUpdationRequest request)
        {
            var feedback = await _unitOfWork.Feedbacks.GetByIDAsync(id);
            if (feedback == null) return false;

            _mapper.Map(request, feedback);
            feedback.UpdatedAt = DateTime.Now;

            _unitOfWork.Feedbacks.Update(feedback);
            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var feedback = await _unitOfWork.Feedbacks.GetByIDAsync(id);
            if (feedback == null) return false;

            _unitOfWork.Feedbacks.Delete(feedback);
            return await _unitOfWork.SaveAsync() > 0;
        }
    }
}

