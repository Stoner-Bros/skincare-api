﻿using APP.BLL.Interfaces;
using APP.BLL.UOW;
using APP.Entity.DTOs.Request;
using APP.Entity.DTOs.Response;
using APP.Entity.Entities;
using APP.Utility;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace APP.BLL.Implements
{
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AccountService> _logger;

        public AccountService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<AccountService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> AccountExists(int id)
        {
            var account = await _unitOfWork.Accounts.GetByIDAsync(id);
            return account != null;
        }

        public async Task<bool> AccountExists(string email)
        {
            var account = await _unitOfWork.Accounts.GetByEmailAsync(email);
            return account != null;
        }

        public async Task<AccountResponse?> CreateAsync(AccountCreationRequest request)
        {
            var account = _mapper.Map<Account>(request);
            account.Password = PasswordEncoder.Encode(request.Password);
            account.AccountInfo = _mapper.Map<AccountInfo>(request);

            Account? response = null;
            try
            {
                response = await _unitOfWork.Accounts.CreateAsync(account);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the account at {Time}.", DateTime.Now);
                return null;
            }
            // Add success => send message
            // User message
            //var message = _mapper.Map<UserMessage>(response);
            //_messagePublisher.Publish(MessageQueueConstants.POSTSVC_USER_CREATE_UPDATE_QUEUE, message);
            //_messagePublisher.Publish(MessageQueueConstants.FRIENDSVC_USER_CREATE_UPDATE_QUEUE, message);
            //_messagePublisher.Publish(MessageQueueConstants.GROUPSVC_USER_CREATE_UPDATE_QUEUE, message);
            //_logger.LogInformation("========Create account successfully, user message sent at {Time}.=========", DateTime.Now);

            //// Notification
            //var notification = new NotificationMessage
            //{
            //    Type = NotificationType.WELCOME,
            //    Content = NotificationContent.WELCOME,
            //    UserId = response.AccountId
            //};
            //_messagePublisher.Publish(MessageQueueConstants.NOTI_CREATE_QUEUE, notification);
            //_logger.LogInformation("========Create account successfully, notification message sent at {Time}.=========", DateTime.Now);

            // Return to controller
            return _mapper.Map<AccountResponse>(response);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            bool result = false;
            var account = await _unitOfWork.Accounts.GetByIDAsync(id);

            if (account != null)
            {
                try
                {
                    //_unitOfWork.Accounts.Delete(account);
                    account.IsDeleted = true;
                    account.UpdateAt = DateTime.Now;
                    _unitOfWork.Accounts.Update(account);
                    result = await _unitOfWork.SaveAsync() > 0;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while deleting the account at {Time}.", DateTime.Now);
                    result = false;
                }
            }

            //if (result)
            //{
            //    // Delete success => send message
            //    _messagePublisher.Publish(MessageQueueConstants.POSTSVC_USER_DELETE_QUEUE, id);
            //    _messagePublisher.Publish(MessageQueueConstants.FRIENDSVC_USER_DELETE_QUEUE, id);
            //    _messagePublisher.Publish(MessageQueueConstants.GROUPSVC_USER_DELETE_QUEUE, id);
            //    _logger.LogInformation("========Delete account successfully, user message sent at {Time}.========", DateTime.Now);
            //}
            // Return to controller
            return result;
        }

        public async Task<IEnumerable<AccountResponse>> GetAllAsync()
        {
            var accounts = await _unitOfWork.Accounts.GetAllAsync();
            return _mapper.Map<IEnumerable<AccountResponse>>(accounts);
        }

        public async Task<PaginationModel<AccountResponse>> GetPagedAsync(int pageNumber, int pageSize)
        {
            var query = _unitOfWork.Accounts.GetQueryable()
                                   .Where(a => !a.IsDeleted) // Lọc dữ liệu
                                   .Include(a => a.AccountInfo);  // Truy vấn dữ liệu

            var totalRecords = await query.CountAsync(); // Tổng số bản ghi
            var accounts = await query
                .OrderBy(a => a.AccountId) // Sắp xếp (có thể thay đổi)
                .Skip((pageNumber - 1) * pageSize) // Bỏ qua các bản ghi trước đó
                .Take(pageSize) // Lấy số lượng bản ghi cần lấy
                .ToListAsync();

            return new PaginationModel<AccountResponse>
            {
                Items = _mapper.Map<List<AccountResponse>>(accounts),
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords
            };
        }

        public async Task<AccountResponse?> GetByIDAsync(int id)
        {
            var account = await _unitOfWork.Accounts.GetDetailInfoByIDAsync(id);

            if (account == null)
            {
                return null;
            }

            return _mapper.Map<AccountResponse>(account);
        }

        public async Task<AccountResponse?> GetByEmailAsync(string email)
        {
            var account = await _unitOfWork.Accounts.GetByEmailAsync(email);

            if (account == null)
            {
                return null;
            }

            return _mapper.Map<AccountResponse>(account);
        }

        public async Task<bool> UpdateAsync(int id, AccountUpdationRequest request)
        {
            bool result = false;
            var account = await _unitOfWork.Accounts.GetDetailInfoByIDAsync(id);

            if (account != null)
            {
                try
                {
                    _mapper.Map(request, account);
                    _mapper.Map(request, account.AccountInfo);
                    account.UpdateAt = DateTime.Now;
                    _unitOfWork.Accounts.Update(account);
                    result = await _unitOfWork.SaveAsync() > 0;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while updating the account at {Time}.", DateTime.Now);
                    result = false;
                }
            }

            //if (result)
            //{
            //    // Update success => send message
            //    var message = _mapper.Map<UserMessage>(account);
            //    _messagePublisher.Publish(MessageQueueConstants.POSTSVC_USER_CREATE_UPDATE_QUEUE, message);
            //    _messagePublisher.Publish(MessageQueueConstants.FRIENDSVC_USER_CREATE_UPDATE_QUEUE, message);
            //    _messagePublisher.Publish(MessageQueueConstants.GROUPSVC_USER_CREATE_UPDATE_QUEUE, message);
            //    _logger.LogInformation("========Update account successfully, user message sent at {Time}.========", DateTime.Now);
            //}
            // Return to controller
            return result;
        }
    }
}
