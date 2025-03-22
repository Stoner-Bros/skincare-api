using APP.Entity.DTOs.Request;
using APP.Entity.DTOs.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APP.BLL.Interfaces
{
    public interface ICommentService
    {
        Task<CommentResponse?> GetByIDAsync(int id);
        Task<CommentResponse?> CreateAsync(int accountId, int blogId, CommentCreationRequest request);
        Task<bool> UpdateAsync(int id, CommentUpdationRequest request);
        Task<bool> DeleteAsync(int id);
        Task<PaginationModel<CommentResponse>> GetByBlogIdAsync(int blogId, int pageNumber, int pageSize); // Updated method for pagination
    }
}
