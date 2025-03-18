using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APP.Entity.Entities
{
    [Table("notification", Schema = "dbo")]
    public class Notification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("notification_id")]
        public int NotificationId { get; set; }

        [Column("account_id")]
        public int AccountId { get; set; }

        [Column("title")]
        [MaxLength(255)]
        public string Title { get; set; } = null!;

        [Column("message")]
        public string Message { get; set; } = null!;

        [Column("type")]
        [MaxLength(50)]
        public string Type { get; set; } = null!;

        [Column("is_read")]
        public bool IsRead { get; set; } = false;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        // Navigation property
        public virtual Account Account { get; set; } = null!;
    }
}
