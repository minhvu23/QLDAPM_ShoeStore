using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoesStore.DAL.Models;
using ShoesStore.BLL;
using ShoesStore.DAL;
//using Sek.Module.Payment.VnPay;

namespace ShoesStore.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VnpayController : ControllerBase
    {
        private readonly VNPAYSvc _vnpaysvc;

        public VnpayController(VNPAYSvc vnpaysvc)
        {
            _vnpaysvc = vnpaysvc;
        }

        [HttpPost("create-payment")]
        public IActionResult CreatePayment(PaymentInformationModel model)
        {
            if (!AuthRep.IsUserLoggedIn(HttpContext))
            {
                return Unauthorized("User is not logged in.");
            }
            if (!AuthRep.IsUserAuthorized(HttpContext, 1) && !AuthRep.IsUserAuthorized(HttpContext, 2))
            {
                return StatusCode(StatusCodes.Status403Forbidden, "User is not authorized to access this resource.");
            }
            var paymentUrl = _vnpaysvc.CreatePaymentUrl(model, HttpContext);
            return Redirect(paymentUrl);
        }

        [HttpGet("payment-callback")]
        public IActionResult PaymentCallback()
        {
            if (!AuthRep.IsUserLoggedIn(HttpContext))
            {
                return Unauthorized("User is not logged in.");
            }
            if (!AuthRep.IsUserAuthorized(HttpContext, 1) && !AuthRep.IsUserAuthorized(HttpContext, 2))
            {
                return StatusCode(StatusCodes.Status403Forbidden, "User is not authorized to access this resource.");
            }
            var response = _vnpaysvc.PaymentExecute(Request.Query);
            return Ok(response); // Sửa thành Ok(response)
        }
    }
}
