using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APP.Entity.Entities
{
    [Table("skin_therapist", Schema = "dbo")]
    public class SkinTherapist
    {
        [Key]
        [Column("account_id")]
        public int AccountId { get; set; }

        [Column("specialization")]
        [MaxLength(255)]
        public string? Specialization { get; set; }

        [Column("yoe")]
        public int YearsOfExperience { get; set; }

        [Column("bio")]
        public string? Bio { get; set; }

        [Column("rating")]
        public double Rating { get; set; }

        [Column("is_available")]
        public bool IsAvailable { get; set; } = true;

        // Navigation property
        public virtual Account Account { get; set; } = null!;
        public virtual ICollection<SkinTherapistSchedule> SkinTherapistSchedules { get; set; } = new List<SkinTherapistSchedule>();


        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
