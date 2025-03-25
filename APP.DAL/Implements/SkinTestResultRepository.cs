using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APP.DAL.Interfaces;
using APP.Entity.Entities;

namespace APP.DAL.Implements
{
    public interface ISkinTestResultRepository : IGenericRepository<SkinTestResult, int> { }

    public class SkinTestResultRepository : GenericRepository<AppDbContext, SkinTestResult, int>, ISkinTestResultRepository
    {
        public SkinTestResultRepository(AppDbContext context) : base(context) { }
    }
}
