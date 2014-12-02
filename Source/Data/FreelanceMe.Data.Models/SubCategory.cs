namespace FreelanceMe.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class SubCategory
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; }

        public virtual Category Category { get; set; }
    }
}