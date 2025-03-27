using APP.Entity.DTOs.Request;
using APP.Entity.DTOs.Response;
using System.Threading.Tasks;

namespace APP.BLL.Interfaces
{
    public interface IOverviewService
    {
        Task<OverviewResponse> GetOverviewAsync(OverviewRequest request);
    }
}
