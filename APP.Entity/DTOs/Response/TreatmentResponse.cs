﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Entity.DTOs.Response
{
    public class TreatmentResponse
    {
        public int TreatmentId { get; set; }
        public int ServiceId { get; set; }
        public string TreatmentName { get; set; } = null!;
        public string TreatmentThumbnailUrl { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Duration { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
    }
}
