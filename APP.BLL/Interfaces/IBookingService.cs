using APP.Entity.DTOs.Request;
using APP.Entity.DTOs.Response;
using APP.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.BLL.Interfaces
{
    public interface IBookingService
    {
        Task<PaginationModel<object>> GetAllAsync(int pageNumber, int pageSize);
        Task<PaginationModel<object>> GetAllByCustomerIdAsync(int customerId, int pageNumber, int pageSize);
        Task<PaginationModel<object>> GetAllByEmailAsync(string email, int pageNumber, int pageSize);
        Task<object?> GetByIDAsync(int id);
        Task<object?> CreateAsync(BookingCreationRequest request);
        Task<bool> UpdateAsync(int id, BookingUpdationRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
