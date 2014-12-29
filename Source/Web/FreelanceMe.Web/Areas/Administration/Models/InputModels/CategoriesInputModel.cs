namespace FreelanceMe.Web.Areas.Administration.Models.InputModels
{
    using System.Collections.Generic;
    using FreelanceMe.Web.Areas.Administration.Models.ViewModels;

    public class CategoriesInputModel
    {
        public CategoryInputModel Category { get; set; }

        public IEnumerable<SubcategoryViewModel> Subcategories { get; set; }
    }
}