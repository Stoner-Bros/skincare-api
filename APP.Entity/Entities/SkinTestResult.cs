using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Entity.Entities
{
    [Table("skin_test_result", Schema = "dbo")]
    public class SkinTestResult
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("result_id")]
        public int ResultId { get; set; }

        [Column("customer_id")]
        public int CustomerId { get; set; }

        [Column("guest_id")]
        public int GuestId { get; set; }

        [Column("skin_test_id")]
        public int SkinTestId { get; set; }

        [Column("result")]
        public string Result { get; set; } = null!;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public virtual SkinTest SkinTest { get; set; } = null!;
        public virtual Customer Customer { get; set; } = null!;
        public virtual Guest Guest { get; set; } = null!;
    }
}
