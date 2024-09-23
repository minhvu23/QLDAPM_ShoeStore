using ShoesStore.Common.BLL;
using ShoesStore.Common.Rsp;
using ShoesStore.DAL;
using ShoesStore.DAL.Models;
using System.Linq;

namespace ShoesStore.BLL
{
    public class CategorySvc : GenericSvc<CategoryRep, Category>
    {
        private readonly CategoryRep _categoryRep;

        public CategorySvc()
        {
            _categoryRep = new CategoryRep();
        }

        public SingleRsp CreateCategory(string name, string description)
        {
            var res = new SingleRsp();
            var category = new Category
            {
                Name = name,
                Description = description
            };

            res = _categoryRep.CreateCategory(category);
            return res;
        }

        public SingleRsp UpdateCategory(int id, string name, string description)
        {
            var res = new SingleRsp();
            var category = _categoryRep.All.FirstOrDefault(c => c.CategoryId == id);

            if (category != null)
            {
                category.Name = name;
                category.Description = description;

                res = _categoryRep.UpdateCategory(category);
            }
            else
            {
                res.SetError("Category not found");
            }

            return res;
        }

        public SingleRsp DeleteCategory(int id)
        {
            var res = new SingleRsp();
            var category = _categoryRep.All.FirstOrDefault(c => c.CategoryId == id);

            if (category != null)
            {
                res = _categoryRep.DeleteCategory(category);
            }
            else
            {
                res.SetError("Category not found");
            }

            return res;
        }
    }
}
