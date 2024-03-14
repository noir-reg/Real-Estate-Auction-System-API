using BusinessObjects.Dtos.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Real_Estate_Auction_System_API.Controllers
{
    [Route("api/mails")]
    [ApiController]
    public class MailController : ControllerBase
    {   private readonly IMailService _mailService;
        public MailController(IMailService mailService)
        {
            _mailService = mailService;

        }
        [HttpPost("send-mail")]
        public async Task<IActionResult> SendMail(SendMailDto sendMail)
        {
            return Ok(await _mailService.SendMail(sendMail));
        }
    }
}
