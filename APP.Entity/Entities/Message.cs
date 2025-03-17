using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Entity.Entities
{
    [Table("message", Schema = "dbo")]
    public class Message
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("message_id")]
        public int MessageId { get; set; }

        [ForeignKey("Account")]
        [Column("sender_id")]
        public int SenderId { get; set; }

        [ForeignKey("Account")]
        [Column("receiver_id")]
        public int ReceiverId { get; set; }

        [Column("message")]
        public string Content { get; set; } = null!;

        [Column("message_type")]
        public string MessageType { get; set; } = null!;

        [Column("status")]
        public string Status { get; set; } = null!;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
