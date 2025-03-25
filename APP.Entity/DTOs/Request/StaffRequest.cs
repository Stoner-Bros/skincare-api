using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Entity.DTOs.Request
{
    public class StaffCreationRequest
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email")]
        [MinLength(6, ErrorMessage = "Email has at least 6 characters")]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [Required]
        [MinLength(6, ErrorMessage = "FullName has at least 6 characters")]
        public string FullName { get; set; } = null!;
        public DateOnly StartDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);
    }

    public class StaffUpdationRequest
    {
        [MinLength(6, ErrorMessage = "FullName has at least 6 characters")]
        public string? FullName { get; set; } = null!;
        public string? Avatar { get; set; }

        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone has exactly 10 digits")]
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public DateOnly? Dob { get; set; }
        public string? OtherInfo { get; set; }

        public DateOnly? StartDate { get; set; }
        public bool? IsAvailable { get; set; }
    }
}
