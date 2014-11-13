namespace FreelanceMe.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using FreelanceMe.Data.Common.Repository;
    using FreelanceMe.Data.Models;

    using Microsoft.AspNet.Identity;

    public class HomeController : Controller
    {
        private IRepository<UserProfile> profiles;

        public HomeController(IRepository<UserProfile> profiles)
        {
            this.profiles = profiles;
        }

        public ActionResult Index()
        {
            var userId = this.User.Identity.GetUserId();
            var profile = this.profiles.All().FirstOrDefault(p => p.User.Id == userId);
            if (profile == null && userId != null)
            {
                this.ViewBag.isProfileEmpty = true;
            }

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