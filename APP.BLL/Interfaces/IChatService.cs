using APP.Entity.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.BLL.Interfaces
{
    public interface IChatService
    {
        Task<PaginationModel<object>> GetChatRoomsAsync(int pageNumber, int pageSize);
        Task<object?> GetChatRoomAsync(int threadId);
        Task<object> CreateChatRoomAsync(int customerId);
        Task<object?> JoinChatRoomAsync(int threadId, int staffId);
    }
}
