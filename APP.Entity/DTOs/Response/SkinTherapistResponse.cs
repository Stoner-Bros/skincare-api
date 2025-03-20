using APP.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Entity.DTOs.Response
{
    public class SkinTherapistResponse
    {
        public int AccountId { get; set; }
        public string? Specialization { get; set; }
        public string? Experience { get; set; }
        public string? Introduction { get; set; }
        public string? Bio { get; set; }
        public double Rating { get; set; }
        public bool IsAvailable { get; set; }
        public AccountResponse Account { get; set; } = null!;
    }
}
