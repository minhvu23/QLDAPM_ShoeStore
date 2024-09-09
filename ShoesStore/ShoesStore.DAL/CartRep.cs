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
    public class CartRep : GenericRep<qldaContext, Cart>
    {
        public CartRep() { }
        public SingleRsp AddToCart(Cart cart)
        {
            var res = new SingleRsp();

            if (cart == null)
            {
                res.SetError("Cart không hợp lệ.");
                return res;
            }

            using (var context = new qldaContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.Carts.Add(cart);
                        context.SaveChanges();
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        res.SetError($"Lỗi khi thêm giỏ hàng: {ex.Message}");
                    }
                }
            }

            return res;
        }
        public SingleRsp UpdateCart(Cart cart)
        {
            var res = new SingleRsp();

            if (cart == null)
            {
                res.SetError("Cart không hợp lệ.");
                return res;
            }

            using (var context = new qldaContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var existingCart = context.Carts.Find(cart.CartId);
                        if (existingCart == null)
                        {
                            res.SetError("Cart không tồn tại.");
                            return res;
                        }

                        context.Entry(existingCart).CurrentValues.SetValues(cart);
                        context.SaveChanges();
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        res.SetError($"Lỗi khi cập nhật giỏ hàng: {ex.Message}");
                    }
                }
            }

            return res;
        }
        public SingleRsp DeleteCart(int cartId)
        {
            var res = new SingleRsp();

            using (var context = new qldaContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var cart = context.Carts.Find(cartId);
                        if (cart == null)
                        {
                            res.SetError("Cart không tồn tại.");
                            return res;
                        }

                        context.Carts.Remove(cart);
                        context.SaveChanges();
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        res.SetError($"Lỗi khi xóa giỏ hàng: {ex.Message}");
                    }
                }
            }

            return res;
        }
        public List<CartItem> GetCartItems(int cartId)
        {
            using (var context = new qldaContext())
            {
                return context.CartItems
                    .Where(c => c.CartId == cartId)
                    .Select(c => new CartItem
                    {
                        CartItemId = c.CartItemId,
                        CartId = c.CartId,
                        ProductId = c.ProductId,
                        Quantity = c.Quantity,
                        Price = c.Price,
                        Product = c.Product // Lấy thông tin sản phẩm
                    }).ToList();
            }
        }

        public decimal GetTotalAmount(int cartId)
        {
            using (var context = new qldaContext())
            {
                var total = context.CartItems
                    .Where(c => c.CartId == cartId)
                    .Sum(c => (decimal?)c.Quantity * c.Price); // Chuyển đổi c.Quantity sang decimal?

                return total.GetValueOrDefault(); // Trả về giá trị không nullable hoặc 0 nếu giá trị là null
            }
        }
    }
}
