using APP.BLL.Implements;
using APP.BLL.Interfaces;
using APP.Entity.DTOs.Request;
using Microsoft.AspNetCore.Mvc;

namespace APP.API.Controllers
{
    [Route("api/emails")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService emailService;

        public EmailController(IEmailService emailService)
        {
            this.emailService = emailService;
        }

        [HttpPost("{toEmail}")]
        public async Task<IActionResult> SendEmail(string toEmail, [FromBody] EmailRequest request)
        {
            var placeholders = new Dictionary<string, string>
            {
                { "Subject", request.Subject }
                // Add other placeholders as needed
            };

            await emailService.SendEmail(toEmail, request.EmailType, placeholders);
            return Ok();
        }
    }
}
