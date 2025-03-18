using APP.DAL.Interfaces;
using APP.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.DAL.Implements
{
    public interface IServiceRepository : IGenericRepository<Service, int> { }

    public class ServiceRepository : GenericRepository<AppDbContext, Service, int>, IServiceRepository
    {
        public ServiceRepository(AppDbContext context) : base(context) { }
    }
}
