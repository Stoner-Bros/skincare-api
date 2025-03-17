using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APP.Entity.Entities
{
    [Table("account_info", Schema = "dbo")]
    public class AccountInfo
    {
        [Key]
        [Column("account_id")]
        public int AccountId { get; set; }

        [Column("full_name")]
        [MaxLength(50)]
        public string FullName { get; set; } = null!;

        [Column("avatar")]
        public string? Avatar { get; set; }

        [Column("phone")]
        [MaxLength(10)]
        public string? Phone { get; set; }

        [Column("address")]
        public string? Address { get; set; }

        [Column("dob")]
        public DateOnly? Dob { get; set; }

        [Column("other_info")]
        public string? OtherInfo { get; set; }

        [JsonIgnore]
        public virtual Account Account { get; set; } = null!;
    }
}
