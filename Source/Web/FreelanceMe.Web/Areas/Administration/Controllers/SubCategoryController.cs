namespace FreelanceMe.Web.Areas.Administration.Controllers
{
    using FreelanceMe.Data.Common.Repository;
    using FreelanceMe.Data.Models;
    using FreelanceMe.Web.Areas.Administration.Models.InputModels;
    using System.Linq;
    using System.Web.Mvc;

    public class SubCategoryController : Controller
    {
        private readonly IDeletableEntityRepository<Category> categories;
        private readonly IDeletableEntityRepository<Subcategory> subcategories;

        public SubCategoryController(IDeletableEntityRepository<Category> categories, IDeletableEntityRepository<Subcategory> subcategories)
        {
            this.categories = categories;
            this.subcategories = subcategories;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult Create(SubCategoryInputModel subCategories)
        {
            if (!ModelState.IsValid)
            {
                return this.View(subCategories);
            }

            var subCategoriesDb = this.subcategories.All().Where(c => c.Name == subCategories.Name).FirstOrDefault();
            if (subCategoriesDb != null)
            {
                ModelState.AddModelError("", "SubCategory with the same name already exists!");
                return this.View(subCategories);
            }

            this.subcategories.Add(new Subcategory()
            {
                Name = subCategories.Name,
                Description = subCategories.Description
            });
            this.subcategories.SaveChanges();

            return this.RedirectToAction("Index", "Category");
        }
    }
}