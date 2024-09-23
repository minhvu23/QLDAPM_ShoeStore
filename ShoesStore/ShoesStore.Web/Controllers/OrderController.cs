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
    public class OrderController : ControllerBase
    {
        private readonly OrderSvc _orderSvc;

        public OrderController()
        {
            _orderSvc = new OrderSvc();
        }

        [HttpGet]
        public IActionResult GetAllOrders()
        {
            var res = new SingleRsp();
            res.Data = _orderSvc.All;
            return Ok(res);
        }

        [HttpGet("{id}")]
        public IActionResult GetOrderById(int id)
        {
            var res = new SingleRsp();
            var order = _orderSvc.All.FirstOrDefault(o => o.OrderId == id);
            if (order != null)
            {
                res.Data = order;
            }
            else
            {
                res.SetError("Order not found");
            }
            return Ok(res);
        }

        [HttpPost]
        public IActionResult CreateOrder(int userId, decimal totalAmount, string status)
        {
            var res = _orderSvc.CreateOrder(userId, totalAmount, status);
            return Ok(res);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOrder(int id, decimal totalAmount, string status)
        {
            var res = _orderSvc.UpdateOrder(id, totalAmount, status);
            return Ok(res);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            var res = _orderSvc.DeleteOrder(id);
            return Ok(res);
        }
    }

}
