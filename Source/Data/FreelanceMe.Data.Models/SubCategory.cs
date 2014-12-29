namespace FreelanceMe.Data.Models
{
    public class Subcategory : CategoryBase
    {
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}