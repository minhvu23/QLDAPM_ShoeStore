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
    public class ProductSvc : GenericSvc<ProductRep, Product>
    {
        private readonly ProductRep _productRep;

        public ProductSvc()
        {
            _productRep = new ProductRep();
        }

        public IEnumerable<Product> GetProductsByCategoryId(int categoryId)
        {
            return _productRep.GetProductsByCategoryId(categoryId);
        }

        public IEnumerable<Product> GetProductsSortedByPriceAscending()
        {
            return _productRep.GetProductsSortedByPriceAscending();
        }

        public IEnumerable<Product> GetProductsSortedByPriceDescending()
        {
            return _productRep.GetProductsSortedByPriceDescending();
        }

        public IEnumerable<Product> GetProductsByName(String name)
        {
            return _productRep.GetProductsByName(name);
        }

        public IEnumerable<Product> GetNewestProducts()
        {
            return _productRep.GetNewestProducts();
        }

        public IEnumerable<Product> GetProductsByPriceRange(decimal lowest, decimal highest)
        {
            return _productRep.GetProductsByPriceRange(lowest, highest);
        }

        public SingleRsp CreateProduct(string name, string description, decimal price, int quantity, int? categoryId, string imageUrl)
        {
            var res = new SingleRsp();
            var product = new Product
            {
                Name = name,
                Description = description,
                Price = price,
                Quantity = quantity,
                CategoryId = categoryId,
                ImageUrl = imageUrl,
                CreatedAt = DateTime.Now
            };

            res = _productRep.CreateProduct(product);
            return res;
        }

        public SingleRsp UpdateProduct(int id, string name, string description, decimal price, int quantity, int? categoryId, string imageUrl)
        {
            var res = new SingleRsp();
            var product = _productRep.All.FirstOrDefault(p => p.ProductId == id);

            if (product != null)
            {
                product.Name = name;
                product.Description = description;
                product.Price = price;
                product.Quantity = quantity;
                product.CategoryId = categoryId;
                product.ImageUrl = imageUrl;
                product.UpdatedAt = DateTime.Now;

                res = _productRep.UpdateProduct(product);
            }
            else
            {
                res.SetError("Product not found");
            }

            return res;
        }

        public SingleRsp DeleteProduct(int id)
        {
            var res = new SingleRsp();
            var product = _productRep.All.FirstOrDefault(p => p.ProductId == id);

            if (product != null)
            {
                res = _productRep.DeleteProduct(product);
            }
            else
            {
                res.SetError("Product not found");
            }

            return res;
        }
    }

}
