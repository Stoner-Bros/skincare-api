using APP.Entity.DTOs.Request;
using APP.Entity.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.BLL.Interfaces
{
    public interface ISkinTherapistService
    {
        Task<PaginationModel<SkinTherapistResponse>> GetAllAsync(int pageNumber, int pageSize);
        Task<PaginationModel<SkinTherapistResponse>> GetAllFreeInSlotAsync(DateOnly? date, int[]? timeSlotId, int pageNumber, int pageSize);
        Task<SkinTherapistResponse?> GetByIDAsync(int id);
        Task<SkinTherapistResponse?> CreateAsync(SkinTherapistCreationRequest request);
        Task<bool> UpdateAsync(int id, SkinTherapistUpdationRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
