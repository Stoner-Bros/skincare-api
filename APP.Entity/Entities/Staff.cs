using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APP.Entity.Entities
{
    [Table("staff", Schema = "dbo")]
    public class Staff
    {
        [Key]
        [ForeignKey("Account")]
        [Column("account_id")]
        public int AccountId { get; set; }

        [Column("start_date")]
        public DateTime StartDate { get; set; }

        [Column("is_available")]
        public bool IsAvailable { get; set; } = true;

        // Navigation property
        public virtual Account Account { get; set; } = null!;
    }
}
