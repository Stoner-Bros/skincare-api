using APP.DAL.Interfaces;
using APP.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.DAL.Implements
{
    public interface ISkinTherapistScheduleRepository : IGenericRepository<SkinTherapistSchedule, int>
    {
    }

    public sealed class SkinTherapistScheduleRepository
        : GenericRepository<AppDbContext, SkinTherapistSchedule, int>, ISkinTherapistScheduleRepository
    {
        public SkinTherapistScheduleRepository(AppDbContext context) : base(context)
        {
        }
    }
}
