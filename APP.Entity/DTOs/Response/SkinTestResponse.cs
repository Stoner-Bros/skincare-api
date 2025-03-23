using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APP.Entity.Entities;

namespace APP.Entity.DTOs.Response
{
    public class SkinTestResponse
    {
        public int SkinTestId { get; set; }
        public string TestName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public List<SkinTestQuestionResponse> SkinTestQuestions { get; set; } = new List<SkinTestQuestionResponse>();

    }
}
