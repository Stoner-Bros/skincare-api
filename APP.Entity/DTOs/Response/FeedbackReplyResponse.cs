using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Entity.DTOs.Response
{
    public class FeedbackReplyResponse
    {
        public int FeedbackReplyId { get; set; }
        public string StaffName { get; set; } = null!;
        public int FeedbackId { get; set; }
        public string Reply { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
