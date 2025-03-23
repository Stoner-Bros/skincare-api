using APP.DAL.Interfaces;
using APP.Entity.Entities;

namespace APP.DAL.Implements
{
    public interface ISkinTestQuestionRepository : IGenericRepository<SkinTestQuestion, int> { }

    public class SkinTestQuestionRepository : GenericRepository<AppDbContext, SkinTestQuestion, int>, ISkinTestQuestionRepository
    {
        public SkinTestQuestionRepository(AppDbContext context) : base(context) { }
    }
}
