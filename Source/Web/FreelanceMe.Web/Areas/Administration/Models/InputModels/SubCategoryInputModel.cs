namespace FreelanceMe.Web.Areas.Administration.Models.InputModels
{
    using System.ComponentModel.DataAnnotations;

    public class SubCategoryInputModel
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; }
    }
}