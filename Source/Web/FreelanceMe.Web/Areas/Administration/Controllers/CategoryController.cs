namespace FreelanceMe.Web.Areas.Administration.Controllers
{
    using AutoMapper.QueryableExtensions;
    using FreelanceMe.Data.Common.Repository;
    using FreelanceMe.Data.Models;
    using FreelanceMe.Web.Areas.Administration.Models.InputModels;
    using FreelanceMe.Web.Areas.Administration.Models.ViewModels;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading;
    using System.Web.Mvc;

    public class CategoryController : Controller
    {
        private readonly IDeletableEntityRepository<Category> categories;

        public CategoryController(IDeletableEntityRepository<Category> categories)
        {
            this.categories = categories;
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

            var dbCategories = this.categories
                .All()
                .Where(c => c.Name.Contains(search) || c.Description.Contains(search))
                .OrderBy(c => c.Name)
                .Skip((page - 1) * numberPerPage)
                .Take(numberPerPage)
                .Project()
                .To<CategoryViewModel>()
                .ToList();

            ViewBag.Search = search;
            ViewBag.NumberOfPages = 1;
            if (categoriesCount > 0)
            {
                ViewBag.NumberOfPages = categoriesCount / numberPerPage;
                if (categoriesCount % numberPerPage > 0)
                {
                    ViewBag.NumberOfPages += 1;
                }
            }

            ViewBag.CurrentPage = page;
            ViewBag.NextPage = page + 1;
            ViewBag.PreviousPage = page - 1;

            if (page == ViewBag.NumberOfPages)
            {
                ViewBag.NextPage = page;
            }

            if (page == 1)
            {
                ViewBag.PreviousPage = 1;
            }

            return View(dbCategories);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return this.PartialView("_Create");
        }

        [HttpPost]
        //[ChildActionOnly]
        public ActionResult Create(CategoryInputModel category)
        {
            if (!ModelState.IsValid)
            {
                return this.PartialView("_Create", category);
            }

            var categoryDb = this.categories.All().Where(c => c.Name == category.Name).FirstOrDefault();
            if (categoryDb != null)
            {
                ModelState.AddModelError("", "Category with the same name already exists!");
                return this.PartialView("_Create", category);
            }

            this.categories.Add(new Category()
            {
                Name = category.Name,
                Description = category.Description
            });
            this.categories.SaveChanges();

            return this.Json("OK");
        }
    }
}