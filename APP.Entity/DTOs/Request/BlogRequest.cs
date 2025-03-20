using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Entity.DTOs.Request
{
    public class BlogCreationRequest
    {
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string? ThumbnailUrl { get; set; }
        public string? Tags { get; set; }
    }

    public class BlogUpdationRequest
    {
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string? ThumbnailUrl { get; set; }
        public string? Tags { get; set; }
    }
}
