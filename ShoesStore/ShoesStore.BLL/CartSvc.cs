using ShoesStore.Common.BLL;
using ShoesStore.Common.Req;
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
    public class CartSvc : GenericSvc<CartRep, Cart>
    {
        CartRep req = new CartRep();
        private  CartRep cartRep;

        public CartSvc()
        {
            cartRep= new CartRep();
        }

        public class CartItemDto
        {
            public int CartId { get; set; }
            public int ProductId { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }
        }
        public SingleRsp AddToCart(CartItemDto cartItemDto)
        {
            var res = new SingleRsp();

            if (cartItemDto == null || cartItemDto.Quantity <= 0)
            {
                res.SetError("Thông tin giỏ hàng không hợp lệ.");
                return res;
            }

            var existingCartItem = cartRep.Context.CartItems
                .FirstOrDefault(c => c.CartId == cartItemDto.CartId && c.ProductId == cartItemDto.ProductId);

            using (var tran = cartRep.Context.Database.BeginTransaction())
            {
                try
                {
                    if (existingCartItem != null)
                    {
                        existingCartItem.Quantity += cartItemDto.Quantity;
                        existingCartItem.Price = cartItemDto.Price;
                        cartRep.Context.CartItems.Update(existingCartItem);
                    }
                    else
                    {
                        var newCartItem = new CartItem
                        {
                            CartId = cartItemDto.CartId,
                            ProductId = cartItemDto.ProductId,
                            Quantity = cartItemDto.Quantity,
                            Price = cartItemDto.Price
                        };
                        cartRep.Context.CartItems.Add(newCartItem);
                    }

                    cartRep.Context.SaveChanges();
                    tran.Commit();
                    res.SetMessage("Thêm vào giỏ hàng thành công.");
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    res.SetError($"Lỗi khi thêm sản phẩm vào giỏ hàng: {ex.Message}");
                }
            }

            return res;
        }


        public List<CartItem> GetCartItems(int cartId)
        {
            return cartRep.GetCartItems(cartId);
        }

        public decimal GetTotalAmount(int cartId)
        {
            return cartRep.GetTotalAmount(cartId);
        }


        public SingleRsp UpdateCart(CartItemDto cartItemDto)
        {
            var res = new SingleRsp();

            if (cartItemDto == null)
            {
                res.SetError("CartItem không hợp lệ.");
                return res;
            }

            var existingCartItem = cartRep.Context.CartItems
                .FirstOrDefault(ci => ci.CartId == cartItemDto.CartId && ci.ProductId == cartItemDto.ProductId);

            if (existingCartItem == null)
            {
                res.SetError("Sản phẩm không tồn tại trong giỏ hàng.");
                return res;
            }

            using (var tran = cartRep.Context.Database.BeginTransaction())
            {
                try
                {
                    existingCartItem.Quantity = cartItemDto.Quantity;
                    existingCartItem.Price = cartItemDto.Price;

                    cartRep.Context.CartItems.Update(existingCartItem);
                    cartRep.Context.SaveChanges();
                    tran.Commit();

                    res.Data = existingCartItem;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    res.SetError($"Lỗi khi cập nhật giỏ hàng: {ex.Message}");
                }
            }

            return res;
        }

        public SingleRsp DeleteCartItem(int cartId, int productId)
        {
            var res = new SingleRsp();

            using (var context = cartRep.Context)
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var cartItem = context.CartItems
                            .FirstOrDefault(ci => ci.CartId == cartId && ci.ProductId == productId);

                        if (cartItem == null)
                        {
                            res.SetError("Sản phẩm không tồn tại trong giỏ hàng.");
                            return res;
                        }

                        context.CartItems.Remove(cartItem);
                        context.SaveChanges();
                        tran.Commit();

                        res.Data = cartItem;
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        res.SetError($"Lỗi khi xóa sản phẩm khỏi giỏ hàng: {ex.Message}");
                    }
                }
            }

            return res;
        }
    }

    


}
