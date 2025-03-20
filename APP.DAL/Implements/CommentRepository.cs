using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APP.DAL.Interfaces;
using APP.Entity.Entities;

namespace APP.DAL.Implements
{
    public interface ICommentRepository : IGenericRepository<Comment, int> { }
    public class CommentRepository : GenericRepository<AppDbContext, Comment, int>, ICommentRepository
    {
        public CommentRepository(AppDbContext context) : base(context) { }
    }
}
