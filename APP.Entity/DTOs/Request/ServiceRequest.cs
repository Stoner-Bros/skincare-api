using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Entity.DTOs.Request
{
    public class ServiceCreationRequest
    {
        public string ServiceName { get; set; } = null!;
        public string ServiceDescription { get; set; } = null!;
    }

    public class ServiceUpdationRequest
    {
        public string ServiceName { get; set; } = null!;
        public string ServiceDescription { get; set; } = null!;
        public bool IsAvailable { get; set; }
    }
}
