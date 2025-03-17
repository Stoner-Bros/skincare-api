using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APP.Entity.Entities
{
    [Table("settings", Schema = "dbo")]
    public class Settings
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("centre_name")]
        [MaxLength(255)]
        public string CentreName { get; set; } = null!;

        [Column("centre_address")]
        [MaxLength(500)]
        public string CentreAddress { get; set; } = null!;

        [Column("centre_phone_number")]
        [MaxLength(20)]
        public string CentrePhoneNumber { get; set; } = null!;

        [Column("centre_email")]
        [MaxLength(100)]
        public string CentreEmail { get; set; } = null!;

        [Column("opening_hours")]
        public TimeSpan OpeningHours { get; set; }

        [Column("closing_hours")]
        public TimeSpan ClosingHours { get; set; }

        [Column("opening_days")]
        [MaxLength(100)]
        public string OpeningDays { get; set; } = null!; // Lưu các ngày mở cửa dưới dạng text

        [Column("description")]
        public string? Description { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
