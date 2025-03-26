using APP.Entity.DTOs.Request;
using APP.Entity.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.BLL.Interfaces
{
    public interface ISkinTherapistScheduleService
    {
        Task<PaginationModel<SkinTherapistScheduleResponse>> GetAllAsync(int therapistId, int pageNumber, int pageSize);
        Task<SkinTherapistScheduleResponse?> GetByIDAsync(int id);
        Task<SkinTherapistScheduleResponse?> CreateAsync(SkinTherapistScheduleCreationRequest request);
        Task<bool> UpdateAsync(int id, SkinTherapistScheduleUpdationRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
