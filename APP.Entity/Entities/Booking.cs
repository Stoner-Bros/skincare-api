using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Entity.Entities
{
    [Table("booking", Schema = "dbo")]
    public class Booking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("booking_id")]
        public int BookingId { get; set; }

        [ForeignKey("Treatment")]
        [Column("treatment_id")]
        public int TreatmentId { get; set; }

        [ForeignKey("SkinTherapist")]
        [Column("skin_therapist_id")]
        public int SkinTherapistId { get; set; }

        [ForeignKey("Staff")]
        [Column("staff_id")]
        public int StaffId { get; set; }

        [ForeignKey("Customer")]
        [Column("customer_id")]
        public int CustomerId { get; set; }

        [ForeignKey("Guest")]
        [Column("guest_id")]
        public int GuestId { get; set; }

        [Column("booking_at")]
        public DateTime BookingAt { get; set; } = DateTime.Now;

        [Column("status")]
        public string Status { get; set; } = "Pending";

        [Column("checkin_at")]
        public DateTime? CheckinAt { get; set; }

        [Column("checkout_at")]
        public DateTime? CheckoutAt { get; set; }

        [Column("total_price")]
        public decimal TotalPrice { get; set; }

        [Column("notes")]
        public string? Notes { get; set; }
    }
}
