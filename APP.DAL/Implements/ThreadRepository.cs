using APP.DAL.Interfaces;
using APP.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.DAL.Implements
{
    public interface IThreadRepository : IGenericRepository<Entity.Entities.Thread, int>
    {
    }

    public sealed class ThreadRepository
        : GenericRepository<AppDbContext, Entity.Entities.Thread, int>, IThreadRepository
    {
        public ThreadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
