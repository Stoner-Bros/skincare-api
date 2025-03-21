using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Entity.DTOs.Request
{
    public class TreatmentRequest
    {
        public int ServiceId { get; set; }
        public string TreatmentName { get; set; } = null!;
        public string TreatmentThumbnailUrl { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Duration { get; set; } = 0;
        public decimal Price { get; set; }
    }
}
