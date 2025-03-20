using APP.Entity.DTOs.Request;
using APP.Entity.DTOs.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APP.BLL.Interfaces
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentResponse>> GetAllAsync();
        Task<CommentResponse?> GetByIDAsync(int id);
        Task<CommentResponse?> CreateAsync(int accountId, int blogId, CommentCreationRequest request);
        Task<bool> UpdateAsync(int id, CommentUpdationRequest request);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<CommentResponse>> GetByBlogIdAsync(int blogId);
    }
}
