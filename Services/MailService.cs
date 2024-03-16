using BusinessObjects.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;
namespace Services
{
    public class MailService : IMailService

    {
        private readonly IConfiguration _configuration;
        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<SendMailDto> SendMail(SendMailDto sendMail)
        {
           

            MailMessage message = new();
            message.From = new MailAddress(_configuration.GetValue<string>("EmailSettings:Email"));
            message.To.Add(sendMail.Email);
            message.Subject = sendMail.Subject;
            message.IsBodyHtml = true;
            message.Body = sendMail.Body;

            SmtpClient client = new(_configuration.GetValue<string>("EmailSettings:Host"), _configuration.GetValue<int>("EmailSettings:Port"));
            client.Credentials = new NetworkCredential(_configuration.GetValue<string>("EmailSettings:Email"), _configuration.GetValue<string>("EmailSettings:Pass"));
            client.EnableSsl = true;
            client.Send(message);
            return sendMail;
        }
    }
}
