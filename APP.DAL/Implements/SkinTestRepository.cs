using APP.DAL.Interfaces;
using APP.Entity.Entities;

namespace APP.DAL.Implements
{
    public interface ISkinTestRepository : IGenericRepository<SkinTest, int> { }

    public class SkinTestRepository : GenericRepository<AppDbContext, SkinTest, int>, ISkinTestRepository
    {
        public SkinTestRepository(AppDbContext context) : base(context) { }
    }
}
