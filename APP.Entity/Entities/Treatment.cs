using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Entity.Entities
{
    [Table("treatment", Schema = "dbo")]
    public class Treatment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("treatment_id")]
        public int TreatmentId { get; set; }

        [ForeignKey("Service")]
        [Column("service_id")]
        public int ServiceId { get; set; }

        [Column("treatment_name")]
        public string TreatmentName { get; set; } = null!;

        [Column("description")]
        public string Description { get; set; } = null!;

        [Column("duration")]
        public int Duration { get; set; }

        [Column("price")]
        public decimal Price { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        [Column("is_available")]
        public bool IsAvailable { get; set; } = true;
    }

}
