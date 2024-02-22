using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Dtos.Request
{
    public class SendToUserDto
    {
        public string Sender { get; set; }
        public string ReceiverId { get; set; }
        public string Message { get; set; }
    }
}
