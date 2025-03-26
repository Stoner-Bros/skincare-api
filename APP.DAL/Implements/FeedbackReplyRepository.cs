using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APP.DAL.Interfaces;
using APP.Entity.Entities;

namespace APP.DAL.Implements
{
    public interface IFeedbackReplyRepository : IGenericRepository<FeedbackReply, int> { }

    public class FeedbackReplyRepository : GenericRepository<AppDbContext, FeedbackReply, int>, IFeedbackReplyRepository
    {
        public FeedbackReplyRepository(AppDbContext context) : base(context) { }
    }
}
