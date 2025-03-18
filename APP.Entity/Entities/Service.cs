using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Entity.Entities
{
    [Table("service", Schema = "dbo")]
    public class Service
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("service_id")]
        public int ServiceId { get; set; }

        [Column("service_name")]
        [MaxLength(100)]
        public string ServiceName { get; set; } = null!;

        [Column("service_description")]
        public string ServiceDescription { get; set; } = null!;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        [Column("is_available")]
        public bool IsAvailable { get; set; } = true;

        public virtual ICollection<Treatment> Treatments { get; set; } = new List<Treatment>();

    }
}
