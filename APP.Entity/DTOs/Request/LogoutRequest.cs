namespace APP.Entity.DTOs.Request
{
    public class LogoutRequest
    {
        public string AccessToken { get; set; } = null!;

        public string RefreshToken { get; set; } = null!;
    }
}
