using APP.DAL.Interfaces;
using APP.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.DAL.Implements
{
    public interface IBookingRepository : IGenericRepository<Booking, int> { }
    public class BookingRepository : GenericRepository<AppDbContext, Booking, int>, IBookingRepository
    {
        public BookingRepository(AppDbContext context) : base(context) { }
    }
}
