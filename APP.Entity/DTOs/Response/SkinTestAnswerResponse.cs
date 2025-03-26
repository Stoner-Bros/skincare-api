using System;

namespace APP.Entity.DTOs.Response
{
    public class SkinTestAnswerResponse
    {
        public int AnswerId { get; set; }
        public int SkinTestId { get; set; }
        public CustomerAnswerResponse? Customer { get; set; }
        public GuestAnswerResponse? Guest { get; set; }
        public string[] Answers { get; set; } = null!;
        public SkinTestResponse SkinTest { get; set; } = null!;
    }

    public class CustomerAnswerResponse
    {
        public int CustomerId { get; set; }
        public string FullName { get; set; } = null!;
        public string Phone { get; set; } = null!;
    }

    public class GuestAnswerResponse
    {
        public int GuestId { get; set; }
        public string FullName { get; set; } = null!;
        public string Phone { get; set; } = null!;
    }
}
