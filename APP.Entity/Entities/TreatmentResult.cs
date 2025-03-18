using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Entity.Entities
{
    [Table("treatment_result", Schema = "dbo")]
    public class TreatmentResult
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("result_id")]
        public int ResultId { get; set; }

        [Column("booking_id")]
        public int BookingId { get; set; }

        [Column("treatment_notes")]
        public string? TreatmentNotes { get; set; }

        [Column("recommendations")]
        public string? Recommendations { get; set; }

        [Column("completed_at")]
        public DateTime? CompletedAt { get; set; }

        public virtual Booking Booking { get; set; } = null!;

    }
}
