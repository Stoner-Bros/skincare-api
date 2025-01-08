using APP.DAL.Interfaces;
using APP.Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace APP.DAL.Implements
{
    public interface IRefeshTokenRepository : IGenericRepository<RefreshToken, int>
    {
        Task<RefreshToken?> GetByRefreshTokenAsync(string token);
    }

    public sealed class RefeshTokenRepository
        : GenericRepository<AppDbContext, RefreshToken, int>, IRefeshTokenRepository
    {
        public RefeshTokenRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<RefreshToken?> GetByRefreshTokenAsync(string token)
            => await _dbSet.Include(s => s.Account).FirstOrDefaultAsync(t => t.Token == token);
    }
}
