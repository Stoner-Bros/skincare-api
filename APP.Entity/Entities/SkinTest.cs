using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Entity.Entities
{
    [Table("skin_test", Schema = "dbo")]
    public class SkinTest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("skin_test_id")]
        public int SkinTestId { get; set; }

        [Column("test_name")]
        public string TestName { get; set; } = null!;

        [Column("description")]
        public string Description { get; set; } = null!;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public virtual ICollection<SkinTestResult> SkinTestResults { get; set; } = new List<SkinTestResult>();

        public virtual ICollection<SkinTestQuestion> SkinTestQuestions { get; set; } = new List<SkinTestQuestion>();

    }
}
