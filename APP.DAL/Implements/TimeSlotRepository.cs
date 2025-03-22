using APP.DAL.Interfaces;
using APP.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.DAL.Implements
{
    public interface ITimeSlotRepository : IGenericRepository<TimeSlot, int> { }

    public class TimeSlotRepository : GenericRepository<AppDbContext, TimeSlot, int>, ITimeSlotRepository
    {
        public TimeSlotRepository(AppDbContext context) : base(context) { }
    }
}
