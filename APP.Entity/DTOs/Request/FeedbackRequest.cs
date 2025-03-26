using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Entity.DTOs.Request
{
    public class FeedbackCreationRequest
    {
        public int Rating { get; set; }
        public string Comment { get; set; } = null!;
    }
    public class FeedbackUpdationRequest
    {
        public int Rating { get; set; }
        public string Comment { get; set; } = null!;
    }
}
