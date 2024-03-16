using BusinessObjects.Dtos.Request;
using BusinessObjects.Dtos.Response;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
                Currency = "vnd",
                Description = "Payment for service",
                Source = paymentRequest.StripeToken
            };

            var service = new ChargeService();
            var charge = service.Create(options);

            // Payment successful
            return new PaymentResponse
            {
                Data = new PaymentInfo
                {
                    PaymentId = charge.Id,
                    PaymentMethod = charge.PaymentMethod,
                    Amount = charge.Amount,
                    Currency = charge.Currency,
                    CustomerId = charge.CustomerId,
                    Status = charge.Status
                },
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
    public PaymentInfo GetPaymentInfo(string paymentId)
    {
        try
        {
            var service = new ChargeService();
            var charge = service.Get(paymentId);
            return new PaymentInfo
            {
                PaymentId = charge.Id,
                PaymentMethod = charge.PaymentMethod,
                Amount = charge.Amount,
                Currency = charge.Currency,
                CustomerId = charge.CustomerId,
                Status = charge.Status
            };
        }
        catch (StripeException ex)
        {

            throw new Exception(ex.Message);
        }
    }
    public List<PaymentInfo> GetAllPaymentInfo()
    {
        try
        {
            var service = new ChargeService();
            var charges = service.List().Select(x => new PaymentInfo
            {
                PaymentId = x.Id,
                PaymentMethod = x.PaymentMethod,
                Amount = x.Amount,
                Currency = x.Currency,
                CustomerId = x.CustomerId,
                Status = x.Status
            }).ToList();
            return charges;
        }
        catch (StripeException ex)
        {

            throw new Exception(ex.Message);
        }
    }
}
