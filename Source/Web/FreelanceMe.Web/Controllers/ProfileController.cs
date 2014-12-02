namespace FreelanceMe.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;
    using FreelanceMe.Data.Common.Repository;
    using FreelanceMe.Data.Models;
    using FreelanceMe.Web.InputModels.Profile;
    using HtmlCustomHelpers.Server;
    using Microsoft.AspNet.Identity;
    using FreelanceMe.Web.ViewModels.Profile;

    [Authorize]
    public class ProfileController : Controller
    {
        private const string ProfilePicturePath = "~/Images/ProfilePictures";

        private readonly IRepository<ApplicationUser> users;
        private readonly IDeletableEntityRepository<UserProfile> profiles;

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

        [AllowAnonymous]
        public ActionResult ProfileHomeDetails()
        {
            var profiles = this.profiles.All().OrderBy(p => p.CreatedOn).Take(5).Project().To<ProfileHomeViewModel>().ToList();
            foreach (var profile in profiles)
            {
                if (profile.Avatar != null)
                {
                    profile.Avatar = "\\Images\\ProfilePictures\\" + profile.Avatar;
                }
            }

            return PartialView(profiles);
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
            var profileDb = this.profiles.GetById(userId);

            if (profileDb == null)
            {
                this.profiles.Add(new UserProfile
                {
                    Id = userId,
                    FirstName = profile.FirstName,
                    LastName = profile.LastName,
                    Country = profile.Country,
                    City = profile.City,
                    Avatar = profile.Avatar
                });
            }
            else
            {
                profileDb.FirstName = profile.FirstName;
                profileDb.LastName = profile.LastName;
                profileDb.Country = profile.Country;
                profileDb.City = profile.City;
                profileDb.Avatar = profile.Avatar;
                this.profiles.Update(profileDb);
            }

            this.profiles.SaveChanges();

            return this.RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult AvatarLoadAjax()
        {
            var userId = this.User.Identity.GetUserId();
            var profileDb = this.profiles.GetById(userId);

            string imageName = null;
            Avatar avatar = new Avatar();

            if (profileDb != null && !string.IsNullOrEmpty(profileDb.Avatar))
            {
                imageName = profileDb.Avatar;
            }

            var loadResult = avatar.LoadFromFile(Server.MapPath(ProfilePicturePath), imageName);
            return this.Json(loadResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AvatarPostAjax(string data)
        {
            Avatar avatar = new Avatar();
            var fileName = avatar.SaveToFile(data, Server.MapPath(ProfilePicturePath), this.User.Identity.GetUserId());
            return this.Json(fileName);
        }

        [HttpPost]
        public ActionResult AvatarDeleteAjax(string fileName, string token)
        {
            Avatar avatar = new Avatar();
            var result = avatar.DeleteFile(Request, fileName, token);

            return this.Json(result);
        }
    }
}