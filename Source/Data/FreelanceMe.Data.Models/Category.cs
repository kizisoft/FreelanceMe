namespace FreelanceMe.Data.Models
{
    using System.Collections.Generic;

    public class Category : CategoryBase
    {
        public virtual ICollection<SubCategory> SubCategories { get; set; }
    }
}