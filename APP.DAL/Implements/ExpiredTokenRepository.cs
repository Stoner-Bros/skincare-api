using APP.DAL.Interfaces;
using APP.Entity.Entities;

namespace APP.DAL.Implements
{
    public interface IExpiredTokenRepository : IGenericRepository<ExpiredToken, string>
    {
    }

    public sealed class ExpiredTokenRepository
        : GenericRepository<AppDbContext, ExpiredToken, string>, IExpiredTokenRepository
    {
        public ExpiredTokenRepository(AppDbContext context) : base(context)
        {
        }
    }
}
