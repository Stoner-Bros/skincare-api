using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Entity.DTOs.Response
{
    public class StaffResponse
    {
        public int AccountId { get; set; }
        public DateOnly StartDate { get; set; }
        public bool IsAvailable { get; set; }
        public AccountResponse Account { get; set; } = null!;
    }
}
