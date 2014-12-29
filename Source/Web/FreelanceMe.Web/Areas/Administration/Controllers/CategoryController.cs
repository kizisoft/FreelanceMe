namespace FreelanceMe.Web.Areas.Administration.Controllers
{
    using AutoMapper.QueryableExtensions;
    using FreelanceMe.Data.Common.Models;
    using FreelanceMe.Data.Common.Repository;
    using FreelanceMe.Data.Models;
    using FreelanceMe.Web.Areas.Administration.Models.InputModels;
    using FreelanceMe.Web.Areas.Administration.Models.ViewModels;
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Web.Mvc;

    public class CategoryController : Controller
    {
        private readonly IDeletableEntityRepository<Category> categories;
        private readonly IDeletableEntityRepository<Subcategory> subcategories;

        public CategoryController(IDeletableEntityRepository<Category> categories, IDeletableEntityRepository<Subcategory> subcategories)
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
            return this.PartialView("_CreatePartial");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryInputModel category)
        {
            if (!ModelState.IsValid)
            {
                return this.PartialView("_CreatePartial", category);
            }

            var categoryDb = this.categories.All().Where(c => c.Name == category.Name).FirstOrDefault();
            if (categoryDb != null)
            {
                ModelState.AddModelError("", "Category with the same name already exists!");
                return this.PartialView("_CreatePartial", category);
            }

            this.categories.Add(new Category()
            {
                Name = category.Name,
                Description = category.Description
            });
            this.categories.SaveChanges();

            return this.Json("OK");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var category = this.categories.All().Where(c => c.Id == id).Project().To<CategoryViewModel>().FirstOrDefault();
            var subcategoriesDb = this.subcategories.All().Where(s => s.CategoryId == id).Project().To<SubcategoryViewModel>().ToList();

            if (category == null)
            {
                return this.RedirectToAction("Index");
            }

            return this.PartialView("_DeletePartial", new CategoriesViewModel { Category = category, Subcategories = subcategoriesDb });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete()
        {
            string idString = this.RouteData.Values["id"].ToString();
            int id = 0;

            if (!int.TryParse(idString, out id))
            {
                return Json(new { error = string.Format("Category with ID = {0} does not exists!", idString) });
            }

            var subcategoriesDb = this.subcategories.All().Where(s => s.CategoryId == id).ToList();
            foreach (var subcategory in subcategoriesDb)
            {
                this.subcategories.Delete(subcategory);
            }

            this.subcategories.SaveChanges();

            this.categories.Delete(id);
            this.categories.SaveChanges();

            return this.Json("OK");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var categoryDb = this.categories.All().Where(c => c.Id == id).Project().To<CategoryViewModel>().FirstOrDefault();
            if (categoryDb == null)
            {
                return this.RedirectToAction("Index");
            }

            var subcategoriesDb = this.subcategories.All().Where(s => s.CategoryId == id).Project().To<SubcategoryViewModel>().ToList();

            return this.PartialView("_EditPartial", new CategoriesViewModel { Category = categoryDb, Subcategories = subcategoriesDb });
        }
    }
}