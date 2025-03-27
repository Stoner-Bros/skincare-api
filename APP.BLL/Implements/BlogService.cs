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
    public class BlogService : IBlogService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<BlogService> _logger;

        public BlogService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<BlogService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PaginationModel<BlogResponse>> GetAllAsync(int pageNumber, int pageSize)
        {
            var query = _unitOfWork.Blogs.GetQueryable()
                .Include(b => b.Account)
                .ThenInclude(a => a.AccountInfo);

            var totalRecords = await query.CountAsync();
            var blogs = await query
                .OrderBy(b => b.BlogId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var blogResponses = _mapper.Map<List<BlogResponse>>(blogs);

            return new PaginationModel<BlogResponse>
            {
                Items = blogResponses,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords
            };
        }

        public async Task<PaginationModel<BlogResponse>> GetPublishedBlogsAsync(int pageNumber, int pageSize)
        {
            var query = _unitOfWork.Blogs.GetQueryable()
                .Include(b => b.Account)
                .ThenInclude(a => a.AccountInfo)
                .Where(b => b.PublishAt != null);

            var totalRecords = await query.CountAsync();
            var blogs = await query
                .OrderBy(b => b.BlogId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var blogResponses = _mapper.Map<List<BlogResponse>>(blogs);

            return new PaginationModel<BlogResponse>
            {
                Items = blogResponses,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords
            };
        }

        public async Task<BlogResponse?> GetByIDAsync(int id)
        {
            var blog = await _unitOfWork.Blogs.GetByIDAsync(id);
            var accountInfo = await _unitOfWork.AccountInfos.GetByIDAsync(blog.AccountId);
            var response = blog == null ? null : _mapper.Map<BlogResponse>(blog);
            if (response == null) return null;
            response.AuthorName = accountInfo?.FullName;
            return response;
        }

        public async Task<BlogResponse?> CreateAsync(int accountId, BlogCreationRequest request)
        {
            var blog = _mapper.Map<Blog>(request);
            blog.AccountId = accountId;
            var createdBlog = await _unitOfWork.Blogs.CreateAsync(blog);
            await _unitOfWork.SaveAsync();

            var accountInfo = await _unitOfWork.AccountInfos.GetByIDAsync(accountId);
            var response = _mapper.Map<BlogResponse>(createdBlog);
            response.AuthorName = accountInfo?.FullName;

            return response;
        }

        public async Task<bool> UpdateAsync(int id, BlogUpdationRequest request)
        {
            var blog = await _unitOfWork.Blogs.GetByIDAsync(id);
            if (blog == null) return false;

            _mapper.Map(request, blog);
            blog.UpdatedAt = DateTime.Now;

            _unitOfWork.Blogs.Update(blog);
            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var blog = await _unitOfWork.Blogs.GetByIDAsync(id);
            if (blog == null) return false;
            //_unitOfWork.Blogs.Delete(blog);
            blog.IsDeleted = true;
            _unitOfWork.Blogs.Update(blog);
            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<bool> PublishAsync(int id)
        {
            var blog = await _unitOfWork.Blogs.GetByIDAsync(id);
            if (blog == null) return false;

            if (blog.PublishAt != null)
            {
                throw new InvalidOperationException("Already Published");
            }

            blog.PublishAt = DateTime.Now;

            _unitOfWork.Blogs.Update(blog);
            return await _unitOfWork.SaveAsync() > 0;
        }
    }
}
