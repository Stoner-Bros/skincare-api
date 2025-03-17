namespace APP.Entity.DTOs.Request
{
    public class AccountCreationRequest
    {
        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
