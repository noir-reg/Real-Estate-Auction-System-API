﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Dtos.Request
{
    public class SendChatMessageDto
    {
        public string UserName { get; set; }
        public string Message { get; set; }
    }
}
