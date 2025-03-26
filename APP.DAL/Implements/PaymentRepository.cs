using APP.DAL.Interfaces;
using APP.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.DAL.Implements
{
    public interface IPaymentRepository : IGenericRepository<Payment, int>
    {
    }

    public sealed class PaymentRepository
        : GenericRepository<AppDbContext, Payment, int>, IPaymentRepository
    {
        public PaymentRepository(AppDbContext context) : base(context)
        {
        }
    }
}
