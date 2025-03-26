using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Entity.DTOs.Response
{
    public class FeedbackResponse
    {
        public int FeedbackId { get; set; }
        public int BookingId { get; set; }
        public string FeedbackBy { get; set; } = null!;
        public int Rating { get; set; }
        public string Comment { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<FeedbackReplyResponse> FeedbackReplies { get; set; } = new List<FeedbackReplyResponse>();
    }
}
