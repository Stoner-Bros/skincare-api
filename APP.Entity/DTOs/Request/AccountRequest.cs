using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Entity.DTOs.Request
{
    public class AccountCreationRequest
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FullName { get; set; } = null!;
    }

    public class AccountUpdationRequest
    {
        public string? FullName { get; set; } = null!;
        public string? Avatar { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public DateOnly? Dob { get; set; }
        public string? OtherInfo { get; set; }
    }
}
