using APP.Entity.DTOs.Request;
using APP.Entity.DTOs.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APP.BLL.Interfaces
{
    public interface ISkinTestQuestionService
    {
        Task<IEnumerable<SkinTestQuestionResponse>> GetAllAsync();
        Task<SkinTestQuestionResponse?> GetByIDAsync(int id);
        Task<SkinTestQuestionResponse?> CreateAsync(int skinTestId, SkinTestQuestionCreationRequest request); // Update this method
        Task<SkinTestQuestionResponse?> UpdateAsync(int id, SkinTestQuestionUpdationRequest request);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<SkinTestQuestionResponse>> GetBySkinTestIdAsync(int skinTestId);
    }
}
