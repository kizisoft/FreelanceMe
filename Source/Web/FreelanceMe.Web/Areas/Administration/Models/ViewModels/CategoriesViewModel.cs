using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreelanceMe.Web.Areas.Administration.Models.ViewModels
{
    public class CategoriesViewModel
    {
        public CategoryViewModel Category { get; set; }

        public IEnumerable<SubcategoryViewModel> Subcategories { get; set; }
    }
}