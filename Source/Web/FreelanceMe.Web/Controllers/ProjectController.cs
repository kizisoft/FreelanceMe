namespace FreelanceMe.Web.Controllers
{
    using FreelanceMe.Web.InputModels.Project;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    [Authorize]
    public class ProjectController : Controller
    {
        [HttpGet]
        public ActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult Create(ProjectInputModel project)
        {
            if (!ModelState.IsValid)
            {
                return this.View(project);
            }

            return this.View();
        }
    }
}