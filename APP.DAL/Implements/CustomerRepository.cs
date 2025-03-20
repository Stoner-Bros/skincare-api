using APP.DAL.Interfaces;
using APP.Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace APP.DAL.Implements
{
    public interface ICustomerRepository : IGenericRepository<Customer, int>
    {
        Task<Customer?> GetDetailInfoByIDAsync(int id);
    }

    public class CustomerRepository : GenericRepository<AppDbContext, Customer, int>, ICustomerRepository
    {
        public CustomerRepository(AppDbContext context) : base(context) { }

        public async Task<Customer?> GetDetailInfoByIDAsync(int id)
            => await _dbSet.Include(a => a.Account)
                    .ThenInclude(b => b.AccountInfo)
                    .FirstOrDefaultAsync(a => a.AccountId == id);
    }
}
