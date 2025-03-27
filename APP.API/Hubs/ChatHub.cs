using APP.DAL;
using Microsoft.AspNetCore.SignalR;

namespace APP.API.Hubs
{
    public class ChatHub : Hub
    {
        private readonly AppDbContext _context;

        public ChatHub(AppDbContext context)
        {
            _context = context;
        }

        public async Task SendMessage(int threadId, int senderId, string senderRole, string message)
        {
            var newMessage = new Entity.Entities.Message
            {
                ThreadId = threadId,
                SenderId = senderId,
                SenderRole = senderRole,
                Content = message,
                Timestamp = DateTime.Now
            };

            _context.Messages.Add(newMessage);
            await _context.SaveChangesAsync();

            await Clients.Group(threadId.ToString()).SendAsync("ReceiveMessage", senderId, senderRole, message);
        }

        public async Task JoinRoom(int threadId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, threadId.ToString());
        }

        public async Task LeaveRoom(int threadId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, threadId.ToString());
        }
    }
}
