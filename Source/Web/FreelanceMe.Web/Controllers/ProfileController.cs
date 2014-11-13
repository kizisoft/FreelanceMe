namespace FreelanceMe.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using FreelanceMe.Data.Common.Repository;
    using FreelanceMe.Data.Models;
    using FreelanceMe.Web.ViewModels.Profile;

    using Microsoft.AspNet.Identity;

    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IRepository<ApplicationUser> users;
        private readonly IDeletableEntityRepository<UserProfile> profiles;

        // private Guid userId;

        public ProfileController(IRepository<ApplicationUser> users, IDeletableEntityRepository<UserProfile> profiles)
        {
            this.users = users;
            this.profiles = profiles;
        }

        public ActionResult Details()
        {
            var userId = this.User.Identity.GetUserId();
            var profile = this.profiles.All().Where(p => p.Id == userId).Project().To<ProfileInputViewModel>().FirstOrDefault();
            if (profile == null)
            {
                return this.RedirectToAction("Edit");
            }

            return this.View(profile);
        }

        [HttpGet]
        public ActionResult Edit()
        {
            var userId = this.User.Identity.GetUserId();
            var profile = this.profiles.All().Where(p => p.Id == userId).Project().To<ProfileInputViewModel>().FirstOrDefault();
            return this.View(profile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProfileInputViewModel profile)
        {
            if (!ModelState.IsValid)
            {
                return this.View(profile);
            }

            var userId = this.User.Identity.GetUserId();
            this.profiles.Add(new UserProfile
            {
                Id = userId,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Country = profile.Country,
                City = profile.City
            });
            this.profiles.SaveChanges();

            return this.RedirectToAction("Index", "Home");
        }
    }
}