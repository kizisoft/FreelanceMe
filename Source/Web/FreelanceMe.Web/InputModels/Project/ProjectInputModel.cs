namespace FreelanceMe.Web.InputModels.Project
{
    using System.ComponentModel.DataAnnotations;
    using FreelanceMe.Data.Models;

    public class ProjectInputModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [StringLength(5000)]
        public string Description { get; set; }

        public Category Category { get; set; }

        public SubCategory SubCategory { get; set; }

        public int? Duration { get; set; }

        public int? Progress { get; set; }
    }
}