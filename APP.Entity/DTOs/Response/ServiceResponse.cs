using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Entity.DTOs.Response
{
    public class ServiceResponse
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; } = null!;
        public string ServiceDescription { get; set; } = null!;
        public bool IsAvailable { get; set; }
    }
}
