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

        [Column("thread_id")]
        public int ThreadId { get; set; }

        [Column("sender_id")]
        public int SenderId { get; set; }

        [Column("sender_role")]
        public string SenderRole { get; set; } = null!;

        [Column("content")]
        public string Content { get; set; } = null!;

        [Column("timestamp")]
        public DateTime Timestamp { get; set; } = DateTime.Now;

        public virtual Thread Thread { get; set; } = null!;
    }
}
