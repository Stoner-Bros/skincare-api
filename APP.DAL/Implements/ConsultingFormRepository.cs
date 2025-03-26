using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APP.DAL.Interfaces;
using APP.Entity.Entities;

namespace APP.DAL.Implements
{
    public interface IConsultingFormRepository : IGenericRepository<ConsultingForm, int> { }
    public class ConsultingFormRepository : GenericRepository<AppDbContext, ConsultingForm, int>, IConsultingFormRepository
    {
        public ConsultingFormRepository(AppDbContext context) : base(context) { }
    }
}
