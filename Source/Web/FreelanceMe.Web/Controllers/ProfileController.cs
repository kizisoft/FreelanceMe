namespace FreelanceMe.Web.Controllers
{
    using System;
    using System.Collections;
    using System.Linq;
    using System.Web.Helpers;
    using System.Web.Mvc;
    using System.Drawing;
    using System.IO;
    using System.Web;

    using AutoMapper.QueryableExtensions;

    using FreelanceMe.Data.Common.Repository;
    using FreelanceMe.Data.Models;
    using FreelanceMe.Web.InputModels.Profile;

    using HtmlCustomHelpers;

    using Microsoft.AspNet.Identity;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

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
            if (profile == null)
            {
                profile = new ProfileInputViewModel();
            }

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

        [HttpPost]
        public ActionResult AvatarPostAjax(string data)
        {
            var indexOfBase64 = data.IndexOf("base64,");
            var base64 = data.Substring(indexOfBase64 + 7);

            using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(base64)))
            {
                using (FileStream fs = new FileStream(Server.MapPath("~/Images/fileImg.jpg"), FileMode.Create))
                {
                    ms.WriteTo(fs);
                }
            }

            return Json("OK");
        }

        [HttpPost]
        public ActionResult AvatarDeleteAjax(string name, string token)
        {
            AntiForgery.Validate(Request.Cookies["__RequestVerificationToken"].Value, token);
            return Json("OK");
        }
    }
}