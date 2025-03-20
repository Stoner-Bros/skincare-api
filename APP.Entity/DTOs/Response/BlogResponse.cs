using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Entity.DTOs.Response
{
    public class BlogResponse
    {
        public int BlogId { get; set; }
        public int AccountId { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? PublishAt { get; set; }
        public string? ThumbnailUrl { get; set; }
        public int ViewCount { get; set; }
        public string? Tags { get; set; }
        public bool IsDeleted { get; set; }
    }
}
