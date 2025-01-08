using APP.DAL.Interfaces;
using APP.Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace APP.DAL.Implements
{
    public interface IAccountRepository : IGenericRepository<Account, int>
    {
        Task<Account?> GetByEmailAsync(string email);
        Task<Account?> GetDetailInfoByIDAsync(int id);
    }

    public sealed class AccountRepository
        : GenericRepository<AppDbContext, Account, int>, IAccountRepository
    {
        public AccountRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Account?> GetByEmailAsync(string email)
            => await _dbSet.FirstOrDefaultAsync(a => a.Email == email);

        public async Task<Account?> GetDetailInfoByIDAsync(int id)
            => await _dbSet.Include(a => a.AccountInfo)
                           .FirstOrDefaultAsync(a => a.AccountId == id);
    }
}
