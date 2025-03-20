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

        public async Task<IEnumerable<CommentResponse>> GetAllAsync()
        {
            var comments = await _unitOfWork.Comments.GetAllAsync();
            return _mapper.Map<IEnumerable<CommentResponse>>(comments);
        }

        public async Task<CommentResponse?> GetByIDAsync(int id)
        {
            var comment = await _unitOfWork.Comments.GetByIDAsync(id);
            return comment == null ? null : _mapper.Map<CommentResponse>(comment);
        }

        public async Task<CommentResponse?> CreateAsync(int accountId, int blogId, CommentCreationRequest request)
        {
            var comment = _mapper.Map<Comment>(request);
            comment.AccountId = accountId;
            comment.BlogId = blogId;
            var createdComment = await _unitOfWork.Comments.CreateAsync(comment);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<CommentResponse>(createdComment);
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
            _unitOfWork.Comments.Delete(comment);
            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<IEnumerable<CommentResponse>> GetByBlogIdAsync(int blogId)
        {
            var comments = await _unitOfWork.Comments.GetQueryable()
                .Where(c => c.BlogId == blogId && !c.IsDeleted)
                .ToListAsync();
            return _mapper.Map<IEnumerable<CommentResponse>>(comments);
        }
    }
}
