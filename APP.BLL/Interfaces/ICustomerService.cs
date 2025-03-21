using APP.Entity.DTOs.Request;
using APP.Entity.DTOs.Response;

namespace APP.BLL.Interfaces
{
    public interface ICustomerService
    {
        Task<PaginationModel<CustomerResponse>> GetAllAsync(int pageNumber, int pageSize);
        Task<CustomerResponse?> GetByIDAsync(int id);
        Task<CustomerResponse?> CreateAsync(AccountCreationRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
