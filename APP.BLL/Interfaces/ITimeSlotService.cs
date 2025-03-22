using APP.Entity.DTOs.Request;
using APP.Entity.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.BLL.Interfaces
{
    public interface ITimeSlotService
    {
        Task<IEnumerable<TimeSlotResponse>> GetAllAsync();
        Task<TimeSlotResponse?> GetByIDAsync(int id);
        Task<bool> InitAgainAsync(TimeSlotInitRequest request);
        Task<bool> Toogle(int id, string note);
        Task<bool> DeleteMultipleAsync(int[] ids);
    }
}
