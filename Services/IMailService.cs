using BusinessObjects.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IMailService
    {
        Task<SendMailDto> SendMail(SendMailDto sendMail);
    }
}
