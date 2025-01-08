using APP.DAL.Interfaces;
using APP.Entity.Entities;

namespace APP.DAL.Implements
{
    public interface IAccountInfoRepository : IGenericRepository<AccountInfo, int>
    {
    }

    public sealed class AccountInfoRepository
        : GenericRepository<AppDbContext, AccountInfo, int>, IAccountInfoRepository
    {
        public AccountInfoRepository(AppDbContext context) : base(context)
        {
        }
    }
}
