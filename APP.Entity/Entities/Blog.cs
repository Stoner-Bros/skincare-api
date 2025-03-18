using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace APP.Entity.Entities
{
    [Table("blog", Schema = "dbo")]
    public class Blog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("blog_id")]
        public int BlogId { get; set; }

        [Column("account_id")]
        public int AccountId { get; set; }

        [Column("title")]
        [MaxLength(255)]
        public string Title { get; set; } = null!;

        [Column("content")]
        public string Content { get; set; } = null!;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [Column("publish_at")]
        public DateTime? PublishAt { get; set; }

        [Column("thumbnail_url")]
        public string? ThumbnailUrl { get; set; }

        [Column("view_count")]
        public int ViewCount { get; set; } = 0;

        [Column("tags")]
        public string? Tags { get; set; }

        [Column("is_deleted")]
        public bool IsDeleted { get; set; } = false;

        // Navigation properties
        public virtual Account Account { get; set; } = null!;
        public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
    }
}
