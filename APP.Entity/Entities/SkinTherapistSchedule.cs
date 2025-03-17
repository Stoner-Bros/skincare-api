using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Entity.Entities
{
    [Table("skin_therapist_schedule", Schema = "dbo")]
    public class SkinTherapistSchedule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("schedule_id")]
        public int ScheduleId { get; set; }

        [Column("skin_therapist_id")]
        public int SkinTherapistId { get; set; }

        [Column("work_date")]
        public DateTime WorkDate { get; set; }

        [Column("start_time")]
        public TimeSpan StartTime { get; set; }

        [Column("end_time")]
        public TimeSpan EndTime { get; set; }

        [Column("is_available")]
        public bool IsAvailable { get; set; } = true;

        [Column("notes")]
        public string? Notes { get; set; }
    }
}
