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
        [EmailAddress(ErrorMessage = "Invalid email")]
        [MinLength(6, ErrorMessage = "Email has at least 6 characters")]
        [Required]
        public string Email { get; set; } = null!;

        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone has exactly 10 digits")]
        [Required]
        public string Phone { get; set; } = null!;

        [MinLength(6, ErrorMessage = "FullName has at least 6 characters")]
        [Required]
        public string FullName { get; set; } = null!;


        [Required]
        public int TreatmentId { get; set; }
        public int? SkinTherapistId { get; set; }
        public string? Notes { get; set; }

        [Required]
        public DateOnly Date { get; set; }
        [Required]
        public int[] TimeSlotIds { get; set; } = null!;
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
