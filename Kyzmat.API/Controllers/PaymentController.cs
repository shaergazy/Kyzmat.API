using Kyzmat.API.Extensions;
using Kyzmat.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kyzmat.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        /// <summary>
        /// Debits 1.10 USD from the user's balance and creates a record of the payment.
        /// </summary>
        /// <returns></returns>
        [HttpPost("makePayment")]
        public async Task<IActionResult> MakePayment()
        {
            var userId = User.GetUserId();
            if (userId == null)
                return Unauthorized("Invalid or missing user ID");

            try
            {
                var success = await _paymentService.MakePaymentAsync(userId.Value.ToString());

                if (!success)
                    return BadRequest("Payment failed");

                return Ok(new { message = "Payment successful" });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { error = "Internal server error" });
            }
        }
    }
}
