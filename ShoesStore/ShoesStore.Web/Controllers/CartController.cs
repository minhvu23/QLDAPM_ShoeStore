using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoesStore.BLL;
using ShoesStore.Common.Req;
using ShoesStore.DAL.Models;
using System;
using static ShoesStore.BLL.CartSvc;

namespace ShoesStore.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {

        private  readonly CartSvc cartSvc;

        // Dependency Injection CartSvc
        public CartController()
        {
            cartSvc = new CartSvc();
        }

        [HttpGet("get-all")]
        public IActionResult GetCartAll()
        {
            var res = cartSvc.All;
            return Ok(res);
        }
        [HttpPost("add-to-cart")]
        public IActionResult AddToCart([FromBody] CartItemDto cartItemDto)
        {
            var result = cartSvc.AddToCart(cartItemDto);
            if (result.Success)
            {
                return Ok(new { success = true });
            }
            return BadRequest(new { success = false, message = result.Message });
        }


        [HttpPut("update-cart")]
        public IActionResult UpdateCart([FromBody] CartItemDto cartItemDto)
        {
            try
            {
                var response = cartSvc.UpdateCart(cartItemDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet("{cartId}/items")]
        public IActionResult GetCartItems(int cartId)
        {
            var cartItems = cartSvc.GetCartItems(cartId);
            if (cartItems != null && cartItems.Count > 0)
            {
                return Ok(cartItems);
            }
            return NotFound(new { message = "Giỏ hàng trống!" });
        }

        [HttpGet("{cartId}/total")]
        public IActionResult GetTotalAmount(int cartId)
        {
            var totalAmount = cartSvc.GetTotalAmount(cartId);
            return Ok(new { totalAmount });
        }
        [HttpDelete("delete-cart-item")]
        public IActionResult DeleteCartItem(int cartId, int productId)
        {
            try
            {
                var response = cartSvc.DeleteCartItem(cartId, productId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
