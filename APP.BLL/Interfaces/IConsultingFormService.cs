using APP.Entity.DTOs.Request;
using APP.Entity.DTOs.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APP.BLL.Interfaces
{
    public interface IConsultingFormService
    {
        Task<PaginationModel<object>> GetAllAsync(int pageNumber, int pageSize);
        Task<object?> GetByIDAsync(int id);
        Task<bool> CreateAsync(ConsultingFormCreationRequest request);
        Task<bool> UpdateAsync(int id, ConsultingFormUpdationRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
