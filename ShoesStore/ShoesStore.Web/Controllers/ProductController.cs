using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoesStore.BLL;
using ShoesStore.Common.Rsp;
using ShoesStore.DAL;
using ShoesStore.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

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

        [HttpGet("all")]
        public IActionResult GetAllProducts()
        {
            var res = new SingleRsp();
            res.Data = _productSvc.All;
            return Ok(res);
        }

        [HttpGet("sorted-by-price-ascending")]
        public IActionResult GetProductsSortedByPriceAscending()
        {
            var products = _productSvc.GetProductsSortedByPriceAscending();
            return Ok(products);
        }
        
        [HttpGet("sorted-by-price-descending")]
        public IActionResult GetProductsSortedByPriceDescending()
        {
            var products = _productSvc.GetProductsSortedByPriceDescending();
            return Ok(products);
        }

        [HttpGet("find/{name}")]
        public IActionResult GetProductsByName(String name)
        {
            var res = new SingleRsp();
            var products = _productSvc.GetProductsByName(name);
            return Ok(products);
        }

        [HttpGet("find/{id:int}")]
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

        [HttpGet("get-newest-products")]
        public IActionResult GetNewestProducts()
        {
            var res = new SingleRsp();
            var products = _productSvc.GetNewestProducts();
            return Ok(products);
        }

        [HttpGet("get-products-from-{lowest}-to-{highest}")]
        public IActionResult GetNewestProducts(decimal lowest, decimal highest)
        {
            var res = new SingleRsp();
            var products = _productSvc.GetProductsByPriceRange(lowest, highest);
            return Ok(products);
        }

        [HttpPost("Create")]
        public IActionResult CreateProduct(string name, string description, decimal price, int quantity, int? categoryId, string imageUrl)
        {
            if (!AuthRep.IsUserLoggedIn(HttpContext))
            {
                return Unauthorized("User is not logged in.");
            }
            if (!AuthRep.IsUserAuthorized(HttpContext, 1))
            {
                return StatusCode(StatusCodes.Status403Forbidden, "User is not authorized to access this resource.");
            }
            var res = _productSvc.CreateProduct(name, description, price, quantity, categoryId, imageUrl);
            return Ok(res);
        }

        [HttpPut("Update/{id}")]
        public IActionResult UpdateProduct(int id, string name, string description, decimal price, int quantity, int? categoryId, string imageUrl)
        {
            if (!AuthRep.IsUserLoggedIn(HttpContext))
            {
                return Unauthorized("User is not logged in.");
            }
            if (!AuthRep.IsUserAuthorized(HttpContext, 1))
            {
                return StatusCode(StatusCodes.Status403Forbidden, "User is not authorized to access this resource.");
            }
            var res = _productSvc.UpdateProduct(id, name, description, price, quantity, categoryId, imageUrl);
            return Ok(res);
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteProduct(int id)
        {
            if (!AuthRep.IsUserLoggedIn(HttpContext))
            {
                return Unauthorized("User is not logged in.");
            }
            if (!AuthRep.IsUserAuthorized(HttpContext, 1))
            {
                return StatusCode(StatusCodes.Status403Forbidden, "User is not authorized to access this resource.");
            }
            var res = _productSvc.DeleteProduct(id);
            return Ok(res);
        }
    }

}
