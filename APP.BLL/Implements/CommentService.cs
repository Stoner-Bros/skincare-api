using APP.BLL.Interfaces;
using APP.BLL.UOW;
using APP.Entity.DTOs.Request;
using APP.Entity.DTOs.Response;
using APP.Entity.Entities;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APP.BLL.Implements
{
    public class CommentService : ICommentService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CommentService> _logger;

        public CommentService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CommentService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CommentResponse?> GetByIDAsync(int id)
        {
            var comment = await _unitOfWork.Comments.GetQueryable()
                .Include(c => c.Account)
                .ThenInclude(a => a.AccountInfo)
                .FirstOrDefaultAsync(c => c.CommentId == id);
            return comment == null ? null : _mapper.Map<CommentResponse>(comment);
        }

        public async Task<CommentResponse?> CreateAsync(int accountId, int blogId, CommentCreationRequest request)
        {
            var comment = _mapper.Map<Comment>(request);
            comment.AccountId = accountId;
            comment.BlogId = blogId;
            var createdComment = await _unitOfWork.Comments.CreateAsync(comment);
            await _unitOfWork.SaveAsync();

            var accountInfo = await _unitOfWork.AccountInfos.GetByIDAsync(accountId);
            var response = _mapper.Map<CommentResponse>(createdComment);
            response.AuthorName = accountInfo?.FullName;

            return response;
        }

        public async Task<bool> UpdateAsync(int id, CommentUpdationRequest request)
        {
            var comment = await _unitOfWork.Comments.GetByIDAsync(id);
            if (comment == null) return false;

            _mapper.Map(request, comment);
            comment.UpdatedAt = DateTime.Now;

            _unitOfWork.Comments.Update(comment);
            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var comment = await _unitOfWork.Comments.GetByIDAsync(id);
            if (comment == null) return false;
            //_unitOfWork.Comments.Delete(comment);
            comment.IsDeleted = true;
            _unitOfWork.Comments.Update(comment);
            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<PaginationModel<CommentResponse>> GetByBlogIdAsync(int blogId, int pageNumber, int pageSize)
        {
            var query = _unitOfWork.Comments.GetQueryable()
                .Include(c => c.Account)
                .ThenInclude(a => a.AccountInfo)
                .Where(c => c.BlogId == blogId && !c.IsDeleted);

            var totalRecords = await query.CountAsync();
            var comments = await query
                .OrderBy(c => c.CommentId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var commentResponses = _mapper.Map<List<CommentResponse>>(comments);

            return new PaginationModel<CommentResponse>
            {
                Items = commentResponses,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords
            };
        }
    }
}
