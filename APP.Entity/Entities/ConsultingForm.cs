using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APP.Entity.Entities
{
    [Table("consulting_form", Schema = "dbo")]
    public class ConsultingForm
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("consulting_form_id")]
        public int ConsultingFormId { get; set; }

        [Column("staff_id")]
        public int? StaffId { get; set; }

        [Column("customer_id")]
        public int? CustomerId { get; set; }  // CustomerId có thể là null nếu khách không đăng ký

        [Column("guest_id")]
        public int? GuestId { get; set; }  // GuestId có thể là null nếu là khách hàng đã đăng ký

        [Column("message")]
        public string Message { get; set; } = null!;

        [Column("status")]
        public string Status { get; set; } = "Pending"; // Trạng thái mặc định là Pending

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;

        // Navigation properties
        public virtual Staff? Staff { get; set; } = null!;
        public virtual Customer? Customer { get; set; }
        public virtual Guest? Guest { get; set; }
    }
}
