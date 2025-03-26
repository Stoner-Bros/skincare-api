using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APP.DAL.Interfaces;
using APP.Entity.Entities;

namespace APP.DAL.Implements
{
    public interface IFeedbackRepository : IGenericRepository<Feedback, int> { }

    public class FeedbackRepository : GenericRepository<AppDbContext, Feedback, int>, IFeedbackRepository
    {
        public FeedbackRepository(AppDbContext context) : base(context) { }
    }
}
