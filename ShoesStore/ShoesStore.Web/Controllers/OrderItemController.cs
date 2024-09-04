using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoesStore.BLL;
using ShoesStore.Common.Rsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesStore.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly OrderItemSvc _orderItemSvc;

        public OrderItemController()
        {
            _orderItemSvc = new OrderItemSvc();
        }

        [HttpGet("order/{orderId}")]
        public IActionResult GetOrderItemsByOrderId(int orderId)
        {
            var res = new SingleRsp();
            var orderItems = _orderItemSvc.GetOrderItemsByOrderId(orderId);
            if (orderItems != null && orderItems.Any())
            {
                res.Data = orderItems;
            }
            else
            {
                res.SetError("No order items found for the specified order.");
            }
            return Ok(res);
        }

        [HttpPost]
        public IActionResult CreateOrderItem(int orderId, int productId, int quantity, decimal price)
        {
            var res = _orderItemSvc.CreateOrderItem(orderId, productId, quantity, price);
            return Ok(res);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOrderItem(int id, int quantity, decimal price)
        {
            var res = _orderItemSvc.UpdateOrderItem(id, quantity, price);
            return Ok(res);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrderItem(int id)
        {
            var res = _orderItemSvc.DeleteOrderItem(id);
            return Ok(res);
        }
    }

}
