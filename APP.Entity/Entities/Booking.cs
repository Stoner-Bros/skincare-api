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

        [Column("treatment_id")]
        public int TreatmentId { get; set; }

        [Column("skin_therapist_id")]
        public int SkinTherapistId { get; set; }

        [Column("staff_id")]
        public int StaffId { get; set; }

        [Column("customer_id")]
        public int CustomerId { get; set; }

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

        public virtual Treatment Treatment { get; set; } = null!;
        public virtual TreatmentResult TreatmentResult { get; set; } = null!;
        public virtual SkinTherapist SkinTherapist { get; set; } = null!;
        public virtual Staff Staff { get; set; } = null!;
        public virtual Customer Customer { get; set; } = null!;
        public virtual Guest Guest { get; set; } = null!;
        public virtual Feedback Feedback { get; set; } = null!;
        public virtual Payment Payment { get; set; } = null!;


        public virtual ICollection<BookingTimeSlot> BookingTimeSlots { get; set; } = new List<BookingTimeSlot>();


    }
}
