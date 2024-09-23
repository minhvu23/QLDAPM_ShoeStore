using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoesStore.BLL;
using ShoesStore.Common.Req;
using ShoesStore.DAL;

namespace ShoesStore.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private UsersSvc UsersSvc;
        public RegisterController()
        {
            UsersSvc = new UsersSvc();
        }
        [HttpPost("")]
        public IActionResult CreateUser([FromBody] CreateUserReq reqUser)
        {
            var res = UsersSvc.CreateUser(reqUser);
            return Ok(res);
        }
    }
}
