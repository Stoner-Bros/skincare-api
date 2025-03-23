using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Entity.DTOs.Request
{
    public class SkinTestCreationRequest
    {
        public string TestName { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
    public class SkinTestUpdationRequest
    {
        public string TestName { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
