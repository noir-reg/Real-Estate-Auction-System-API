﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Dtos.Request
{
    public class PaymentRequest
    {
        public int Amount { get; set; }
        public string StripeToken { get; set; }
    }
}
