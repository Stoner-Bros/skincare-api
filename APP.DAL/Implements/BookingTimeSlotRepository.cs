using APP.DAL.Interfaces;
using APP.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.DAL.Implements
{
    public interface IBookingTimeSlotRepository : IGenericRepository<BookingTimeSlot, int>
    {
    }

    public sealed class BookingTimeSlotRepository
        : GenericRepository<AppDbContext, BookingTimeSlot, int>, IBookingTimeSlotRepository
    {
        public BookingTimeSlotRepository(AppDbContext context) : base(context)
        {
        }
    }
}
