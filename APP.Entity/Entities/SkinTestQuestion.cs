using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Entity.Entities
{
    [Table("skin_test_question", Schema = "dbo")]
    public class SkinTestQuestion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("skin_test_question_id")]
        public int SkinTestQuestionId { get; set; }

        [Column("skin_test_id")]
        public int SkinTestId { get; set; }

        [Column("question_text")]
        public string QuestionText { get; set; } = null!;

        [Column("option_a")]
        public string OptionA { get; set; } = null!;

        [Column("option_b")]
        public string OptionB { get; set; } = null!;

        [Column("option_c")]
        public string OptionC { get; set; } = null!;

        [Column("option_d")]
        public string OptionD { get; set; } = null!;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public virtual SkinTest SkinTest { get; set; } = null!;

    }
}
