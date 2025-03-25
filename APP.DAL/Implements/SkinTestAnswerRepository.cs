using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APP.DAL.Interfaces;
using APP.Entity.Entities;

namespace APP.DAL.Implements
{
    public interface ISkinTestAnswerRepository : IGenericRepository<SkinTestAnswer, int> { }
    public class SkinTestAnswerRepository : GenericRepository<AppDbContext, SkinTestAnswer, int>, ISkinTestAnswerRepository
    {
        public SkinTestAnswerRepository(AppDbContext context) : base(context)
        {
        }
    }
}
