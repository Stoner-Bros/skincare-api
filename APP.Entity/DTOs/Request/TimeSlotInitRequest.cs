using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Entity.DTOs.Request
{
    public class TimeSlotInitRequest
    {
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        public double GapTime { get; set; }
    }
}
