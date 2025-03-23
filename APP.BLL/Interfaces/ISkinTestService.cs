using APP.Entity.DTOs.Request;
using APP.Entity.DTOs.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APP.BLL.Interfaces
{
    public interface ISkinTestService
    {
        Task<IEnumerable<SkinTestResponse>> GetAllAsync();
        Task<SkinTestResponse?> GetByIDAsync(int id);
        Task<SkinTestResponse?> CreateAsync(SkinTestCreationRequest request);
        Task<SkinTestResponse?> UpdateAsync(int id, SkinTestUpdationRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
