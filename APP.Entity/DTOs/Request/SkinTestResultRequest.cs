using System;

namespace APP.Entity.DTOs.Request
{
    public class SkinTestResultRequest
    {
        public int? CustomerId { get; set; }
        public int? GuestId { get; set; }
        public string Result { get; set; } = null!;
    }
}
