using APP.Entity.DTOs.Request;
using APP.Entity.DTOs.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APP.BLL.Interfaces
{
    public interface IBlogService
    {
        Task<IEnumerable<BlogResponse>> GetAllAsync();
        Task<BlogResponse?> GetByIDAsync(int id);
        Task<BlogResponse?> CreateAsync(int accountId, BlogCreationRequest request);
        Task<bool> UpdateAsync(int id, BlogUpdationRequest request);
        Task<bool> DeleteAsync(int id);
        Task<bool> PublishAsync(int id);
    }

}