using APP.Entity.DTOs.Request;
using APP.Entity.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.BLL.Interfaces
{
    public interface ITreatmentService
    {
        Task<PaginationModel<TreatmentResponse>> GetAllAsync(int serviceId, int pageNumber, int pageSize);
        Task<TreatmentResponse?> GetByIDAsync(int id);
        Task<TreatmentResponse?> CreateAsync(TreatmentRequest request);
        Task<bool> UpdateAsync(int id, TreatmentRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
