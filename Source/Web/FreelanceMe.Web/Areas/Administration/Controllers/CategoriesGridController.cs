namespace FreelanceMe.Web.Areas.Administration.Controllers
{

    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using FreelanceMe.Data.Common.Repository;
    using FreelanceMe.Data.Models;
    using FreelanceMe.Web.Areas.Administration.Models.ViewModels;

    public class CategoriesGridController : Controller
    {
        private readonly IDeletableEntityRepository<Category> categories;
        private readonly IDeletableEntityRepository<Subcategory> subcategories;

        public CategoriesGridController(IDeletableEntityRepository<Category> categories, IDeletableEntityRepository<Subcategory> subcategories)
        {
            this.categories = categories;
            this.subcategories = subcategories;
        }

        [HttpGet]
        public ActionResult Index(string search, int page = 1, int numberPerPage = 5)
        {
            if (search == null)
            {
                search = "";
            }

            var categoriesCount = this.categories
                .All()
                .Where(c => c.Name.Contains(search) || c.Description.Contains(search))
                .OrderBy(c => c.Name)
                .Count();

            var categoriesDb = this.categories
                .All()
                .Where(c => c.Name.Contains(search) || c.Description.Contains(search))
                .OrderBy(c => c.Name)
                .Skip((page - 1) * numberPerPage)
                .Take(numberPerPage)
                .Project()
                .To<CategoryBaseViewModel>()
                .ToList();

            return PartialView("_IndexPartial", categoriesDb);
        }
    }
}