using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Entity.Entities
{
    [Table("threads", Schema = "dbo")]
    public class Thread
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("thread_id")]
        public int ThreadId { get; set; }

        [Column("customer_id")]
        public int CustomerId { get; set; }

        [Column("staff_id")]
        public int? StaffId { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public virtual Customer Customer { get; set; } = null!;
        public virtual Staff? Staff { get; set; }
        public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}
