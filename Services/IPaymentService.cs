using BusinessObjects.Dtos.Request;
using BusinessObjects.Dtos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IPaymentService
    {
        PaymentResponse ProcessPayment(PaymentRequest paymentRequest);
        public List<PaymentInfo> GetAllPaymentInfo();
        public PaymentInfo GetPaymentInfo(string paymentId);
    }
}
