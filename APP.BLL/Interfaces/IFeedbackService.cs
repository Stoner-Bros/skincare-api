using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APP.Entity.DTOs.Request;
using APP.Entity.DTOs.Response;

namespace APP.BLL.Interfaces
{
    public interface IFeedbackService
    {
        Task<PaginationModel<FeedbackResponse>> GetAllAsync(int pageNumber, int pageSize);
        Task<FeedbackResponse?> GetByIDAsync(int id);
        Task<FeedbackResponse?> GetByBookingIdAsync(int bookingId);
        Task<FeedbackResponse?> CreateAsync(int bookingId, FeedbackCreationRequest request, int accountId);
        Task<bool> UpdateAsync(int id, FeedbackUpdationRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
