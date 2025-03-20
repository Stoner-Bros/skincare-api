using APP.DAL.Interfaces;
using APP.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.DAL.Implements
{
    public interface ITreatmentRepository : IGenericRepository<Treatment, int> { }

    public class TreatmentRepository : GenericRepository<AppDbContext, Treatment, int>, ITreatmentRepository
    {
        public TreatmentRepository(AppDbContext context) : base(context) { }
    }
}
