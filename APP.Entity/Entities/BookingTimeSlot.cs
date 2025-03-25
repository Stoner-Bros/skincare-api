using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Entity.Entities
{
    [Table("booking_time_slot", Schema = "dbo")]
    public class BookingTimeSlot
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("booking_id")]
        public int BookingId { get; set; }

        [Column("time_slot_id")]
        public int TimeSlotId { get; set; }

        [Column("date")]
        public DateOnly Date { get; set; }

        [Column("notes")]
        public string? Notes { get; set; }

        public virtual TimeSlot TimeSlot { get; set; } = null!;
        public virtual Booking Booking { get; set; } = null!;
    }
}
