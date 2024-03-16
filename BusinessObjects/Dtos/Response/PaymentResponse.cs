using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Dtos.Response;

public class PaymentResponse
{
    public PaymentInfo? Data { get; set; }
    public bool isSuccess { get; set; }
    public string Message { get; set; }
}
public class PaymentInfo
{
    public string PaymentId { get; set; }
    public long Amount { get; set; }
    public string Status { get; set; }
    public string Currency { get; set; }
    public string PaymentMethod { get; set; }
    public string CustomerId { get; set; }
}

