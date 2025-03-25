using APP.DAL.Interfaces;
using APP.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.DAL.Implements
{
    public interface IGuestRepository : IGenericRepository<Guest, int>
    {
        Task<Guest?> GetByEmailAsync(string email);
    }

    public sealed class GuestRepository
        : GenericRepository<AppDbContext, Guest, int>, IGuestRepository
    {
        public GuestRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Guest?> GetByEmailAsync(string email)
            => await _dbSet.FirstOrDefaultAsync(a => a.Email == email);
    }
}
