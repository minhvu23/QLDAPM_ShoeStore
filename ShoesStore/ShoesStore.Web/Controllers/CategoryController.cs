using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoesStore.BLL;
using ShoesStore.Common.Rsp;
using System.Linq;

namespace ShoesStore.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategorySvc _categorySvc;

        public CategoryController()
        {
            _categorySvc = new CategorySvc();
        }

        [HttpGet]
        public IActionResult GetAllCategories()
        {
            var res = new SingleRsp();
            var categories = _categorySvc.All;
            res.Data = categories;
            return Ok(res);
        }

        [HttpGet("{id}")]
        public IActionResult GetCategoryById(int id)
        {
            var res = new SingleRsp();
            var category = _categorySvc.All.FirstOrDefault(c => c.CategoryId == id);
            if (category != null)
            {
                res.Data = category;
            }
            else
            {
                res.SetError("Category not found");
            }
            return Ok(res);
        }

        [HttpPost]
        public IActionResult CreateCategory(string name, string description)
        {
            var res = _categorySvc.CreateCategory(name, description);
            return Ok(res);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id, string name, string description)
        {
            var res = _categorySvc.UpdateCategory(id, name, description);
            return Ok(res);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var res = _categorySvc.DeleteCategory(id);
            return Ok(res);
        }
    }
}
