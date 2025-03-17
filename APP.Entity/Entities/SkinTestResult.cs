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

        [ForeignKey("Customer")]
        [Column("customer_id")]
        public int CustomerId { get; set; }

        [ForeignKey("Guest")]
        [Column("guest_id")]
        public int GuestId { get; set; }

        [ForeignKey("SkinTest")]
        [Column("skin_test_id")]
        public int SkinTestId { get; set; }

        [Column("result")]
        public string Result { get; set; } = null!;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
