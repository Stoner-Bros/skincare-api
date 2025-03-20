using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Entity.DTOs.Request
{
    public class CommentCreationRequest
    {
        public string Content { get; set; } = null!;

    }

    public class CommentUpdationRequest
    {
        public string Content { get; set; } = null!;
    }
}
