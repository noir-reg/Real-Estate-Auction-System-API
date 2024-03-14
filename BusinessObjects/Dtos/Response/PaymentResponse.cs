using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Dtos.Response
{
    public class PaymentResponse
    {
        public bool isSuccess { get; set; }
        public string Message { get; set; }
    }
}
