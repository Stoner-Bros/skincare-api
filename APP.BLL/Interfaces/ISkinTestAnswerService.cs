using APP.Entity.DTOs.Request;
using APP.Entity.DTOs.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APP.BLL.Interfaces
{
    public interface ISkinTestAnswerService
    {
        Task<IEnumerable<SkinTestAnswerResponse>> GetAllAsync();
        Task<SkinTestAnswerResponse?> CreateSkinTestAnswerAsync(SkinTestAnswerRequest request);
    }
}
