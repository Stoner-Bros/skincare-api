using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Entity.DTOs.Request
{
    public class SkinTherapistCreationRequest
    {
        public int AccountId { get; set; }
        public string? Specialization { get; set; }
        public string? Experience { get; set; }
        public string? Introduction { get; set; }
        public string? Bio { get; set; }
    }

    public class SkinTherapistUpdationRequest
    {
        public string? Specialization { get; set; }
        public string? Experience { get; set; }
        public string? Introduction { get; set; }
        public string? Bio { get; set; }
        public bool IsAvailable { get; set; }
    }
}
