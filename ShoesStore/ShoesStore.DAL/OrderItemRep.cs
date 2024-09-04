using ShoesStore.Common.DAL;
using ShoesStore.Common.Rsp;
using ShoesStore.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesStore.DAL
{
    public class OrderItemRep : GenericRep<qldaContext, OrderItem>
    {
        public IEnumerable<OrderItem> GetOrderItemsByOrderId(int orderId)
        {
            return All.Where(oi => oi.OrderId == orderId).ToList();
        }
        public SingleRsp CreateOrderItem(OrderItem orderItem)
        {
            var res = new SingleRsp();
            using (var context = new qldaContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.OrderItems.Add(orderItem);
                        context.SaveChanges();
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        res.SetError(ex.StackTrace);
                    }
                }
            }
            return res;
        }

        public SingleRsp UpdateOrderItem(OrderItem orderItem)
        {
            var res = new SingleRsp();
            using (var context = new qldaContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.OrderItems.Update(orderItem);
                        context.SaveChanges();
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        res.SetError(ex.StackTrace);
                    }
                }
            }
            return res;
        }

        public SingleRsp DeleteOrderItem(OrderItem orderItem)
        {
            var res = new SingleRsp();
            using (var context = new qldaContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.OrderItems.Remove(orderItem);
                        context.SaveChanges();
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        res.SetError(ex.StackTrace);
                    }
                }
            }
            return res;
        }
    }

}
