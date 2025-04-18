﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Entity.DTOs.Response
{
    public class SkinTherapistScheduleResponse
    {
        public int ScheduleId { get; set; }
        public DateOnly WorkDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool IsAvailable { get; set; }
        public string? Notes { get; set; }
    }
}
