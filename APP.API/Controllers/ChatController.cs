using APP.API.Controllers.Helper;
using APP.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APP.API.Controllers
{
    [ApiController]
    [Route("api/chat")]
    public class ChatController : ApiBaseController
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpGet("threads")]
        public async Task<IActionResult> GetChatRooms(
                [FromQuery] int pageNumber = 1,
                [FromQuery] int pageSize = 10
            )
        {
            if (pageNumber < 1 || pageSize < 1)
                return ResponseNoData(400, "PageNumber and PageSize must greater than 0.");

            var pagedAccounts = await _chatService.GetChatRoomsAsync(pageNumber, pageSize);

            return ResponseOk(pagedAccounts);
        }

        [HttpGet("threads/{threadId}")]
        public async Task<IActionResult> GetChatRoom(int threadId)
        {
            var chatRoom = await _chatService.GetChatRoomAsync(threadId);
            if (chatRoom == null)
            {
                return _respNotFound;
            }

            return ResponseOk(chatRoom);
        }

        [HttpPost("threads")]
        public async Task<IActionResult> CreateChatRoom([FromBody] int customerId)
        {
            var chatRoom = await _chatService.CreateChatRoomAsync(customerId);
            return ResponseOk(chatRoom);
        }

        [HttpPost("threads/{threadId}/join")]
        public async Task<IActionResult> JoinChatRoom(int threadId, [FromBody] int staffId)
        {
            var chatRoom = await _chatService.JoinChatRoomAsync(threadId, staffId);
            if (chatRoom == null)
            {
                return _respNotFound;
            }

            return ResponseOk(chatRoom);
        }
    }
}
