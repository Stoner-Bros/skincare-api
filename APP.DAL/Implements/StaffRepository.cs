using APP.DAL.Interfaces;
using APP.Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace APP.DAL.Implements
{
    public interface IStaffRepository : IGenericRepository<Staff, int>
    {
        Task<Staff?> GetDetailInfoByIDAsync(int id);
    }

    public class StaffRepository : GenericRepository<AppDbContext, Staff, int>, IStaffRepository
    {
        public StaffRepository(AppDbContext context) : base(context) { }

        public async Task<Staff?> GetDetailInfoByIDAsync(int id)
            => await _dbSet.Include(a => a.Account)
                    .ThenInclude(b => b.AccountInfo)
                    .FirstOrDefaultAsync(a => a.AccountId == id);
    }
}
