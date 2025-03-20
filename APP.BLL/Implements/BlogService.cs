using APP.BLL.Interfaces;
using APP.BLL.UOW;
using APP.Entity.DTOs.Request;
using APP.Entity.DTOs.Response;
using APP.Entity.Entities;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
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

        public async Task<IEnumerable<BlogResponse>> GetAllAsync()
        {
            var blogs = await _unitOfWork.Blogs.GetAllAsync();
            return _mapper.Map<IEnumerable<BlogResponse>>(blogs);
        }

        public async Task<BlogResponse?> GetByIDAsync(int id)
        {
            var blog = await _unitOfWork.Blogs.GetByIDAsync(id);
            return blog == null ? null : _mapper.Map<BlogResponse>(blog);
        }

        public async Task<BlogResponse?> CreateAsync(int accountId, BlogCreationRequest request)
        {
            var blog = _mapper.Map<Blog>(request);
            blog.AccountId = accountId;
            var createdBlog = await _unitOfWork.Blogs.CreateAsync(blog);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<BlogResponse>(createdBlog);
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
            _unitOfWork.Blogs.Delete(blog);
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