using System;

namespace APP.Entity.DTOs.Response
{
    public class SkinTestResultResponse
    {
        public int ResultId { get; set; }
        public int? CustomerId { get; set; }
        public int? GuestId { get; set; }
        public int SkinTestAnswerId { get; set; }
        public string Result { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
