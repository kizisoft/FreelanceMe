namespace FreelanceMe.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;

    using FreelanceMe.Data.Common.Repository;
    using FreelanceMe.Data.Models;
    using FreelanceMe.Web.ViewModels.Home;

    public class HomeController : Controller
    {
        private IRepository<Profile> profiles;

        public HomeController(IRepository<Profile> profiles)
        {
            this.profiles = profiles;
        }

        public ActionResult Index()
        {
            var result = this.profiles.All().Project().To<IndexProfileViewModel>();
            return View(result);
        }
    }
}