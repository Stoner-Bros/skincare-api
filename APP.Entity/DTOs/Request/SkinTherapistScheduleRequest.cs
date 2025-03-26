using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Entity.DTOs.Request
{
    public class SkinTherapistScheduleCreationRequest
    {
        public int SkinTherapistId { get; set; }
        public DateOnly WorkDate { get; set; }
        public int TimeSlotId { get; set; }
        public string? Notes { get; set; }
    }

    public class SkinTherapistScheduleUpdationRequest
    {
        public bool? IsAvailable { get; set; }
        public string? Notes { get; set; }
    }
}
