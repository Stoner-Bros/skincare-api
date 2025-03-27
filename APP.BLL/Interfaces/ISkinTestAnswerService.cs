using APP.Entity.DTOs.Request;
using APP.Entity.DTOs.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APP.BLL.Interfaces
{
    public interface ISkinTestAnswerService
    {
        Task<IEnumerable<SkinTestAnswerResponse>> GetAllAsync();
        Task<SkinTestAnswerResponse?> GetByIdAsync(int id);
        Task<SkinTestAnswerResponse?> CreateSkinTestAnswerAsync(SkinTestAnswerRequest request);
        Task<IEnumerable<SkinTestAnswerResponse>> GetByCustomerId(int customerId);
        Task<bool> DeleteSkinTestAnswerAsync(int id);
    }
}
