using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Entity.DTOs.Request
{
    public class BookingCreationRequest
    {
        [Required]
        public int TreatmentId { get; set; }
        public int? SkinTherapistId { get; set; }
        public string? Notes { get; set; }
        [Required]
        public List<int> TimeSlotIds { get; set; } = new List<int>();
    }

    public class BookingUpdationRequest
    {
        public int? SkinTherapistId { get; set; }
        public int? StaffId { get; set; }
        public string? Status { get; set; } = "Pending";
        public DateTime? CheckinAt { get; set; }
        public DateTime? CheckoutAt { get; set; }
    }
}
