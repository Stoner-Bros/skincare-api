using APP.Entity.DTOs.Request;
using APP.Entity.DTOs.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APP.BLL.Interfaces
{
    public interface IFeedbackReplyService
    {
        Task<IEnumerable<FeedbackReplyResponse>> GetAllAsync();
        Task<IEnumerable<FeedbackReplyResponse>> GetByFeedbackIdAsync(int feedbackId);
        Task<FeedbackReplyResponse?> CreateByFeedbackIdAsync(int feedbackId, FeedbackReplyCreationRequest request, int staffId);
        Task<bool> UpdateAsync(int id, FeedbackReplyUpdationRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
