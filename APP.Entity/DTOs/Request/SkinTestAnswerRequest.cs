using System;
using System.ComponentModel.DataAnnotations;

namespace APP.Entity.DTOs.Request
{
    public class SkinTestAnswerRequest
    {
        public int SkinTestId { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email")]
        [MinLength(6, ErrorMessage = "Email has at least 6 characters")]
        [Required]
        public string Email { get; set; } = null!;

        [MinLength(6, ErrorMessage = "FullName has at least 6 characters")]
        [Required]
        public string FullName { get; set; } = null!;

        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone has exactly 10 digits")]
        [Required]
        public string Phone { get; set; } = null!;
        public string[] Answers { get; set; } = null!;
    }
}
