namespace FreelanceMe.Web.Controllers
{
    using Microsoft.AspNet.Identity;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using FreelanceMe.Data.Common.Repository;
    using FreelanceMe.Data.Models;
    using FreelanceMe.Web.ViewModels.Profiles;

    public class ProfilesController : Controller
    {
        private readonly IRepository<ApplicationUser> users;
        private readonly IDeletableEntityRepository<UserProfile> profiles;

        public ProfilesController(IRepository<ApplicationUser> users, IDeletableEntityRepository<UserProfile> profiles)
        {
            this.users = users;
            this.profiles = profiles;
        }

        public ActionResult Index()
        {
            var userId = this.User.Identity.GetUserId();
            var currentProfileView = this.profiles.All().Project().To<ProfileViewModel>().FirstOrDefault(p => p.User.Id == userId);
            if (currentProfileView == null)
            {
                var currentUser = this.users.GetById(userId);
                currentProfileView = new ProfileViewModel { User = currentUser };
            }

            return View(currentProfileView);
        }

        // GET: Profiles/Details/5
        //        public ActionResult Details(Guid? id)
        //        {
        //            if (id == null)
        //            {
        //                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //            }
        //            UserProfile profile = db.Profiles.Find(id);
        //            if (profile == null)
        //            {
        //                return HttpNotFound();
        //            }
        //            return View(profile);
        //        }

        //        // GET: Profiles/Create
        //        public ActionResult Create()
        //        {
        //            return View();
        //        }

        //        // POST: Profiles/Create
        //        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //        [HttpPost]
        //        [ValidateAntiForgeryToken]
        //        public ActionResult Create([Bind(Include = "Id,UserId,FirstName,LastName,Country,City,Rating,IsDeleted,DeletedOn,CreatedOn,ModifiedOn")] UserProfile profile)
        //        {
        //            if (ModelState.IsValid)
        //            {
        //                profile.Id = Guid.NewGuid();
        //                db.Profiles.Add(profile);
        //                db.SaveChanges();
        //                return RedirectToAction("Index");
        //            }

        //            return View(profile);
        //        }

        //        // GET: Profiles/Edit/5
        //        public ActionResult Edit(Guid? id)
        //        {
        //            if (id == null)
        //            {
        //                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //            }
        //            UserProfile profile = db.Profiles.Find(id);
        //            if (profile == null)
        //            {
        //                return HttpNotFound();
        //            }
        //            return View(profile);
        //        }

        //        // POST: Profiles/Edit/5
        //        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //        [HttpPost]
        //        [ValidateAntiForgeryToken]
        //        public ActionResult Edit([Bind(Include = "Id,UserId,FirstName,LastName,Country,City,Rating,IsDeleted,DeletedOn,CreatedOn,ModifiedOn")] UserProfile profile)
        //        {
        //            if (ModelState.IsValid)
        //            {
        //                db.Entry(profile).State = EntityState.Modified;
        //                db.SaveChanges();
        //                return RedirectToAction("Index");
        //            }
        //            return View(profile);
        //        }

        //        // GET: Profiles/Delete/5
        //        public ActionResult Delete(Guid? id)
        //        {
        //            if (id == null)
        //            {
        //                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //            }
        //            UserProfile profile = db.Profiles.Find(id);
        //            if (profile == null)
        //            {
        //                return HttpNotFound();
        //            }
        //            return View(profile);
        //        }

        //        // POST: Profiles/Delete/5
        //        [HttpPost, ActionName("Delete")]
        //        [ValidateAntiForgeryToken]
        //        public ActionResult DeleteConfirmed(Guid id)
        //        {
        //            UserProfile profile = db.Profiles.Find(id);
        //            db.Profiles.Remove(profile);
        //            db.SaveChanges();
        //            return RedirectToAction("Index");
        //        }

        //        protected override void Dispose(bool disposing)
        //        {
        //            if (disposing)
        //            {
        //                db.Dispose();
        //            }
        //            base.Dispose(disposing);
        //        }
    }
}