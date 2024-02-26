using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Dtos.Request
{
    public class SendToGroupDto
    {
        
        public string GroupId { get; set; }
        public string SenderName { get; set; }
        public string Message { get; set;}

    }
}
