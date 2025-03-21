using APP.Entity.DTOs.Request;
using APP.Entity.DTOs.Response;

namespace APP.BLL.Interfaces
{
    public interface IStaffService
    {
        Task<PaginationModel<StaffResponse>> GetAllAsync(int pageNumber, int pageSize);
        Task<StaffResponse?> GetByIDAsync(int id);
        Task<StaffResponse?> CreateAsync(StaffCreationRequest request);
        Task<bool> UpdateAsync(int id, StaffUpdationRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
