using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Entity.Entities
{
    [Table("feedback_reply", Schema = "dbo")]
    public class FeedbackReply
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("feedback_reply_id")]
        public int FeedbackReplyId { get; set; }

        [ForeignKey("Staff")]
        [Column("staff_id")]
        public int StaffId { get; set; }

        [ForeignKey("Feedback")]
        [Column("feedback_id")]
        public int FeedbackId { get; set; }

        [Column("reply")]
        public string Reply { get; set; } = null!;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
