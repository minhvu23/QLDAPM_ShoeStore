using ShoesStore.Common.BLL;
using ShoesStore.Common.Rsp;
using ShoesStore.DAL;
using ShoesStore.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesStore.BLL
{
    public class OrderSvc : GenericSvc<OrderRep, Order>
    {
        private readonly OrderRep _orderRep;

        public OrderSvc()
        {
            _orderRep = new OrderRep();
        }

        public SingleRsp CreateOrder(int userId, decimal totalAmount, string status)
        {
            var res = new SingleRsp();
            var order = new Order
            {
                UserId = userId,
                TotalAmount = totalAmount,
                Status = status,
                CreatedAt = DateTime.Now
            };

            res = _orderRep.CreateOrder(order);
            return res;
        }

        public SingleRsp UpdateOrder(int id, decimal totalAmount, string status)
        {
            var res = new SingleRsp();
            var order = _orderRep.All.FirstOrDefault(o => o.OrderId == id);

            if (order != null)
            {
                order.TotalAmount = totalAmount;
                order.Status = status;
                order.UpdatedAt = DateTime.Now;

                res = _orderRep.UpdateOrder(order);
            }
            else
            {
                res.SetError("Order not found");
            }

            return res;
        }

        public SingleRsp DeleteOrder(int id)
        {
            var res = new SingleRsp();
            var order = _orderRep.All.FirstOrDefault(o => o.OrderId == id);

            if (order != null)
            {
                res = _orderRep.DeleteOrder(order);
            }
            else
            {
                res.SetError("Order not found");
            }

            return res;
        }
    }

}
