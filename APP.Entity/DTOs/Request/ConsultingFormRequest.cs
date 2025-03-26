using System;

namespace APP.Entity.DTOs.Request
{
    public class ConsultingFormCreationRequest
    {
        public string Message { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string FullName { get; set; } = null!;
    }

    public class ConsultingFormUpdationRequest
    {
        public int? StaffId { get; set; }
        public string? Status { get; set; } = null!;
        public DateTime? UpdatedAt { get; set; }
    }


}
