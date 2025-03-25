using System;

namespace APP.Entity.DTOs.Request
{
    public class SkinTestAnswerRequest
    {
        public int SkinTestId { get; set; }
        public int? CustomerId { get; set; }
        public int? GuestId { get; set; }
        public string[] Answers { get; set; } = null!;
    }
}
