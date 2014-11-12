namespace FreelanceMe.Web.Controllers
{
    using System.Web.Mvc;

    using FreelanceMe.Data.Common.Repository;
    using FreelanceMe.Data.Models;

    public class HomeController : Controller
    {
        private IRepository<UserProfile> profiles;

        public HomeController(IRepository<UserProfile> profiles)
        {
            this.profiles = profiles;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return this.View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return this.View();
        }
    }
}