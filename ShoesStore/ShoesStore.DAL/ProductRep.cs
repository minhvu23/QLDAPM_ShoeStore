﻿using ShoesStore.Common.DAL;
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

        public IEnumerable<Product> GetProductsByCategory(int categoryId)
        {
            var products = Context.Products.Where(p => p.CategoryId == categoryId).ToList();
            return products;
        }

        public IEnumerable<Product> SearchProducts(string keyword, string type)
        {
            var query = All.AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                // Sử dụng ToLower() để thực hiện so sánh không phân biệt chữ hoa/chữ thường
                query = query.Where(p => p.Name.ToLower().Contains(keyword.ToLower()));
            }

            return query.ToList();
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
