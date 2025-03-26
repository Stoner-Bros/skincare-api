using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Entity.DTOs.Request
{
    public class FeedbackReplyCreationRequest
    {
        public string Reply { get; set; } = null!;
    }
    public class FeedbackReplyUpdationRequest
    {
        public string Reply { get; set; } = null!;
    }
}
