using APP.DAL.Interfaces;
using APP.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace APP.DAL.Implements
{
    public interface IBlogRepository : IGenericRepository<Blog, int> { }

    public class BlogRepository : GenericRepository<AppDbContext, Blog, int>, IBlogRepository
    {
        public BlogRepository(AppDbContext context) : base(context) { }
    }
}