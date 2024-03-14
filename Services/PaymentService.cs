using BusinessObjects.Dtos.Request;
using BusinessObjects.Dtos.Response;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services;

public class PaymentService : IPaymentService
{
    public PaymentResponse ProcessPayment(PaymentRequest paymentRequest)
    {
        try
        {
            var options = new ChargeCreateOptions
            {
                Amount = paymentRequest.Amount,
                Currency = "usd",
                Description = "Payment for service",
                Source = paymentRequest.Token
            };

            var service = new ChargeService();
            var charge = service.Create(options);

            // Payment successful
            return new PaymentResponse
            {
                isSuccess = true,
                Message = "Payment successful"
            };
        }
        catch (StripeException ex)
        {
            // Payment failed
            return new PaymentResponse
            {
                isSuccess = false,
                Message = ex.Message
            };
        }
    }
}
