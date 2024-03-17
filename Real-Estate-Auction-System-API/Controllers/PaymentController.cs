using BusinessObjects.Dtos.Request;
using BusinessObjects.Dtos.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Real_Estate_Auction_System_API.Controllers
{
    [Route("api/payments")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        [HttpPost]
        public ActionResult<PaymentResponse> ProcessPayment([FromBody] PaymentRequest paymentRequest)
        {
            var result = _paymentService.ProcessPayment(paymentRequest);
            if (result.isSuccess)
                return Ok(result);
            else return BadRequest(result);
        }
    }
}
