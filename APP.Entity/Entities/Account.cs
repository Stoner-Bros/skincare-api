﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APP.Entity.Entities
{
    [Table("account", Schema = "dbo")]
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("account_id")]
        public int AccountId { get; set; }

        [Column("email")]
        [MaxLength(30)]
        public string Email { get; set; } = null!;

        [Column("password")]
        public string Password { get; set; } = null!;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("updated_at")]
        public DateTime UpdateAt { get; set; } = DateTime.Now;

        [Column("role")]
        public string Role { get; set; } = "Customer";

        [Column("is_deleted")]
        public bool IsDeleted { get; set; } = false;

        public virtual AccountInfo AccountInfo { get; set; } = null!;
        public virtual Customer Customer { get; set; } = null!;
        public virtual Staff Staff { get; set; } = null!;
        public virtual SkinTherapist SkinTherapist { get; set; } = null!;
        public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

        public virtual ICollection<Blog> Blogs { get; set; } = new List<Blog>();

        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

        public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

        public virtual ICollection<ExpiredToken> ExpiredTokens { get; set; } = new List<ExpiredToken>();
    }
}
