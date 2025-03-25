using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Entity.DTOs.Response
{
    public class SkinTestAnswerResponse
    {
        public int AnswerId { get; set; }
        public int SkinTestId { get; set; }
        public int? CustomerId { get; set; }
        public int? GuestId { get; set; }
        public string[] Answers { get; set; } = null!;
        public SkinTestResponse SkinTest { get; set; } = null!;
    }
}
