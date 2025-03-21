using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Entity.DTOs.Response
{
    public class CustomerResponse
    {
        public int AccountId { get; set; }
        public DateTime? LastVisit { get; set; }
        public AccountResponse Account { get; set; } = null!;
    }
}
