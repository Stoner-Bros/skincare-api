using APP.Entity.DTOs.Request;
using APP.Entity.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.BLL.Interfaces
{
    public interface IServiceService
    {
        Task<IEnumerable<ServiceResponse>> GetAllAsync();
        Task<ServiceResponse?> GetByIDAsync(int id);
        Task<ServiceResponse?> CreateAsync(ServiceCreationRequest request);
        Task<bool> UpdateAsync(int id, ServiceUpdationRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
