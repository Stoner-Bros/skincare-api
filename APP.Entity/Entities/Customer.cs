using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APP.Entity.Entities
{
    [Table("customer", Schema = "dbo")]
    public class Customer
    {
        [Key]
        [ForeignKey("Account")]
        [Column("account_id")]
        public int AccountId { get; set; }

        [Column("last_visit")]
        public DateTime? LastVisit { get; set; }

        // Navigation property
        public virtual Account Account { get; set; } = null!;
    }
}
