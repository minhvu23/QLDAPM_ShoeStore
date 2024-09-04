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
    public class ProductController : ControllerBase
    {
        private readonly ProductSvc _productSvc;

        public ProductController()
        {
            _productSvc = new ProductSvc();
        }

        [HttpGet("category/{categoryId}")]
        public IActionResult GetProductsByCategoryId(int categoryId)
        {
            var res = new SingleRsp();
            var products = _productSvc.GetProductsByCategoryId(categoryId);
            if (products != null && products.Any())
            {
                res.Data = products;
            }
            else
            {
                res.SetError("No products found for the specified category.");
            }
            return Ok(res);
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var res = new SingleRsp();
            res.Data = _productSvc.All;
            return Ok(res);
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var res = new SingleRsp();
            var product = _productSvc.All.FirstOrDefault(p => p.ProductId == id);
            if (product != null)
            {
                res.Data = product;
            }
            else
            {
                res.SetError("Product not found");
            }
            return Ok(res);
        }

        [HttpPost]
        public IActionResult CreateProduct(string name, string description, decimal price, int quantity, int? categoryId, string imageUrl)
        {
            var res = _productSvc.CreateProduct(name, description, price, quantity, categoryId, imageUrl);
            return Ok(res);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, string name, string description, decimal price, int quantity, int? categoryId, string imageUrl)
        {
            var res = _productSvc.UpdateProduct(id, name, description, price, quantity, categoryId, imageUrl);
            return Ok(res);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var res = _productSvc.DeleteProduct(id);
            return Ok(res);
        }
    }

}
