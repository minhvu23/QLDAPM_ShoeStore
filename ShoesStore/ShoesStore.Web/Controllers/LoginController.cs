using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoesStore.BLL;
using ShoesStore.Common.Req;
using ShoesStore.DAL.Models;

namespace ShoesStore.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly LoginSvc loginSvc;

        public LoginController()
        {
            loginSvc = new LoginSvc();
        }

        [HttpPost("auth")]
        //public IActionResult Login([FromBody] LoginReq loginReq)
        //{
        //    if (string.IsNullOrEmpty(loginReq.Username) || string.IsNullOrEmpty(loginReq.Password))
        //    {
        //        return BadRequest("Username or password cannot be empty");
        //    }

        //    var res = loginSvc.Authenticate(loginReq.Username, loginReq.Password);

        //    if (res.Success)
        //    {
        //        var user = (User)res.Data;
        //        HttpContext.Session.SetInt32("UserId", user.UserId);
        //        HttpContext.Session.SetString("LoggedInUser", user.Username);
        //        return Ok(res);
        //    }
        //    else
        //    {
        //        return BadRequest(res);
        //    }
        //}
        public IActionResult Login([FromBody] LoginReq loginReq)
        {
            if (string.IsNullOrEmpty(loginReq.Username) || string.IsNullOrEmpty(loginReq.Password))
            {
                return BadRequest(new { success = false, message = "Username or password cannot be empty" });
            }

            var res = loginSvc.Authenticate(loginReq.Username, loginReq.Password);

            if (res.Success)
            {
                var user = (User)res.Data;
                HttpContext.Session.SetInt32("UserId", user.UserId);
                HttpContext.Session.SetString("LoggedInUser", user.Username);

                // Return only necessary information
                return Ok(new
                {
                    success = true,
                    data = new
                    {
                        userId = user.UserId,
                        username = user.Username,
                        email = user.Email // add more fields if needed
                    }
                });
            }
            else
            {
                return BadRequest(new { success = false, message = res.Message });
            }
        }
    }
}
