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
    public class ProductRep : GenericRep<qldaContext, Product>
    {
        public IEnumerable<Product> GetProductsByCategoryId(int categoryId)
        {
            return All.Where(p => p.CategoryId == categoryId).ToList();
        }
        public IEnumerable<Product> GetProductsSortedByPriceAscending()
        {
            return All.OrderBy(p => p.Price).ToList();
        }

        public IEnumerable<Product> GetProductsSortedByPriceDescending()
        {
            return All.OrderByDescending(p => p.Price).ToList();
        }

        public IEnumerable<Product> GetProductsByName(String name)
        {
            return All.Where(p => p.Name.Contains(name)).ToList();
        }

        public IEnumerable<Product> GetNewestProducts()
        {
            return All.OrderByDescending((p) => p.UpdatedAt).ToList();
        }

        public IEnumerable<Product> GetProductsByPriceRange(decimal lowest, decimal highest)
        {
            return All.Where(p => p.Price >= lowest && p.Price <= highest).OrderBy(p => p.Price).ToList();
        }

        public SingleRsp CreateProduct(Product product)
        {
            var res = new SingleRsp();
            using (var context = new qldaContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var p = context.Products.Add(product);
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

        public SingleRsp UpdateProduct(Product product)
        {
            var res = new SingleRsp();
            using (var context = new qldaContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var p = context.Products.Update(product);
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

        public SingleRsp DeleteProduct(Product product)
        {
            var res = new SingleRsp();
            using (var context = new qldaContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var p = context.Products.Remove(product);
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
