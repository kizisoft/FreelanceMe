namespace FreelanceMe.Data.Models
{
    using System.Collections.Generic;

    public class Category : CategoryBase
    {
        public virtual ICollection<Subcategory> SubCategories { get; set; }
    }
}