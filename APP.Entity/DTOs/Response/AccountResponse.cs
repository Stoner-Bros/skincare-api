using APP.Entity.Entities;

namespace APP.Entity.DTOs.Response
{
    public class AccountResponse
    {
        public int AccountId { get; set; }
        public string Email { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public string Role { get; set; } = null!;
        public bool IsDeleted { get; set; } = false;
        public virtual AccountInfo AccountInfo { get; set; } = null!;
    }
}
