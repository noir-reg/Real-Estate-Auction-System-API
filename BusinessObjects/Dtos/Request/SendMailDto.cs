using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Dtos.Request
{
    public class SendMailDto
    {
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

    }
}
