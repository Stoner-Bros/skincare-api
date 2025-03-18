using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APP.Entity.Entities
{
    [Table("guest", Schema = "dbo")]
    public class Guest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("guest_id")]
        public int GuestId { get; set; }

        [Column("email")]
        [MaxLength(100)]
        public string? Email { get; set; }

        [Column("phone")]
        [MaxLength(15)]
        public string? Phone { get; set; }

        [Column("full_name")]
        [MaxLength(50)]
        public string FullName { get; set; } = null!;
        public virtual ICollection<SkinTestResult> SkinTestResults { get; set; } = new List<SkinTestResult>();

        public virtual ICollection<ConsultingForm> ConsultingForms { get; set; } = new List<ConsultingForm>();

        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
