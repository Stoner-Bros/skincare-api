using APP.BLL.Interfaces;
using APP.BLL.UOW;
using APP.DAL;
using APP.Entity.DTOs.Response;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace APP.BLL.Implements
{
    public class ChatService : IChatService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ChatService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginationModel<object>> GetChatRoomsAsync(int pageNumber, int pageSize)
        {
            var query = _unitOfWork.Threads.GetQueryable()
                .Select(cr => new
                {
                    cr.ThreadId,
                    cr.CreatedAt,

                    Customer = new
                    {
                        cr.Customer.AccountId,
                        cr.Customer.Account.AccountInfo.FullName,
                        cr.Customer.Account.AccountInfo.Avatar
                    },

                    Staff = cr.Staff == null ? null : new
                    {
                        cr.StaffId,
                        cr.Staff.Account.AccountInfo.FullName,
                        cr.Staff.Account.AccountInfo.Avatar
                    }
                });

            var totalRecords = await query.CountAsync();
            var blogs = await query
                .OrderBy(b => b.ThreadId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginationModel<object>
            {
                Items = blogs,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords
            };
        }

        public async Task<object?> GetChatRoomAsync(int threadId)
        {
            var query = _unitOfWork.Threads.GetQueryable()
                .Select(cr => new
                {
                    cr.ThreadId,
                    cr.CreatedAt,

                    Customer = new
                    {
                        cr.Customer.AccountId,
                        cr.Customer.Account.AccountInfo.FullName,
                        cr.Customer.Account.AccountInfo.Avatar
                    },

                    Staff = cr.Staff == null ? null : new
                    {
                        cr.StaffId,
                        cr.Staff.Account.AccountInfo.FullName,
                        cr.Staff.Account.AccountInfo.Avatar
                    }
                });

            return await query.FirstOrDefaultAsync(s => s.ThreadId == threadId);
        }

        public async Task<object> CreateChatRoomAsync(int customerId)
        {
            var chatRoom = new Entity.Entities.Thread
            {
                CustomerId = customerId
            };

            await _unitOfWork.Threads.CreateAsync(chatRoom);
            await _unitOfWork.SaveAsync();

            return chatRoom;
        }

        public async Task<object?> JoinChatRoomAsync(int threadId, int staffId)
        {
            var chatRoom = await _unitOfWork.Threads.GetByIDAsync(threadId);
            if (chatRoom == null)
            {
                return null;
            }

            chatRoom.StaffId = staffId;
            await _unitOfWork.SaveAsync();

            return chatRoom;
        }
    }
}
