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
    public class OrderItemSvc : GenericSvc<OrderItemRep, OrderItem>
    {
        private readonly OrderItemRep _orderItemRep;

        public OrderItemSvc()
        {
            _orderItemRep = new OrderItemRep();
        }

        public SingleRsp CreateOrderItem(int orderId, int productId, int quantity, decimal price)
        {
            var res = new SingleRsp();
            var orderItem = new OrderItem
            {
                OrderId = orderId,
                ProductId = productId,
                Quantity = quantity,
                Price = price
            };

            res = _orderItemRep.CreateOrderItem(orderItem);
            return res;
        }

        public IEnumerable<OrderItem> GetOrderItemsByOrderId(int orderId)
        {
            return _orderItemRep.GetOrderItemsByOrderId(orderId);
        }

        public SingleRsp UpdateOrderItem(int orderItemId, int quantity, decimal price)
        {
            var res = new SingleRsp();
            var orderItem = _orderItemRep.All.FirstOrDefault(oi => oi.OrderItemId == orderItemId);

            if (orderItem != null)
            {
                orderItem.Quantity = quantity;
                orderItem.Price = price;

                res = _orderItemRep.UpdateOrderItem(orderItem);
            }
            else
            {
                res.SetError("OrderItem not found");
            }

            return res;
        }

        public SingleRsp DeleteOrderItem(int orderItemId)
        {
            var res = new SingleRsp();
            var orderItem = _orderItemRep.All.FirstOrDefault(oi => oi.OrderItemId == orderItemId);

            if (orderItem != null)
            {
                res = _orderItemRep.DeleteOrderItem(orderItem);
            }
            else
            {
                res.SetError("OrderItem not found");
            }

            return res;
        }
    }

}
