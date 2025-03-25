using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APP.Entity.Entities
{
    [Table("skin_test_answer", Schema = "dbo")]
    public class SkinTestAnswer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("answer_id")]
        public int AnswerId { get; set; }

        [Column("skin_test_id")]
        public int SkinTestId { get; set; }

        [Column("customer_id")]
        public int? CustomerId { get; set; }

        [Column("guest_id")]
        public int? GuestId { get; set; }

        [Column("answers")]
        public string[] Answers { get; set; } = null!;

        public virtual SkinTest SkinTest { get; set; } = null!;
        public virtual Customer? Customer { get; set; }
        public virtual Guest? Guest { get; set; }
        public virtual SkinTestResult SkinTestResults { get; set; } = null!;
    }
}
