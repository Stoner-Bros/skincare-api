using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Entity.Entities
{
    [Table("payment", Schema = "dbo")]
    public class Payment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("payment_id")]
        public int PaymentId { get; set; }

        [Column("booking_id")]
        public int BookingId { get; set; }

        [Column("amount")]
        public decimal Amount { get; set; }

        [Column("payment_method")]
        public string PaymentMethod { get; set; } = null!;

        [Column("payment_status")]
        public string PaymentStatus { get; set; } = "Pending";

        [Column("payment_date")]
        public DateTime PaymentDate { get; set; } = DateTime.Now;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        [Column("transaction_id")]
        public string? TransactionId { get; set; }
        public virtual Booking Booking { get; set; } = null!;
    }
}
