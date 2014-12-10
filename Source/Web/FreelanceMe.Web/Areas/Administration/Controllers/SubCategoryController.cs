namespace FreelanceMe.Web.Areas.Administration.Controllers
{
    using FreelanceMe.Data.Common.Repository;
    using FreelanceMe.Data.Models;
    using FreelanceMe.Web.Areas.Administration.Models.InputModels;
    using System.Linq;
    using System.Web.Mvc;

    public class SubCategoryController : Controller
    {
        private readonly IDeletableEntityRepository<Category> subCategories;

        public SubCategoryController(IDeletableEntityRepository<Category> subCategories)
        {
            this.subCategories = subCategories;
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

            var subCategoriesDb = this.subCategories.All().Where(c => c.Name == subCategories.Name).FirstOrDefault();
            if (subCategoriesDb != null)
            {
                ModelState.AddModelError("", "SubCategory with the same name already exists!");
                return this.View(subCategories);
            }

            this.subCategories.Add(new Category()
            {
                Name = subCategories.Name,
                Description = subCategories.Description
            });
            this.subCategories.SaveChanges();

            return this.RedirectToAction("Index", "Category");
        }
    }
}