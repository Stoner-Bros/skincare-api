using APP.DAL.Interfaces;
using APP.Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace APP.DAL.Implements
{
    public interface ISkinTherapistRepository : IGenericRepository<SkinTherapist, int>
    {
        Task<SkinTherapist?> GetDetailInfoByIDAsync(int id);
    }

    public class SkinTherapistRepository : GenericRepository<AppDbContext, SkinTherapist, int>, ISkinTherapistRepository
    {
        public SkinTherapistRepository(AppDbContext context) : base(context) { }

        public async Task<SkinTherapist?> GetDetailInfoByIDAsync(int id)
            => await _dbSet.Include(a => a.Account)
                    .ThenInclude(b => b.AccountInfo)
                    .FirstOrDefaultAsync(a => a.AccountId == id);
    }
}
