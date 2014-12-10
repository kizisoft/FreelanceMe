namespace FreelanceMe.Web.Areas.Administration.Models.InputModels
{
    using System.ComponentModel.DataAnnotations;

    public class CategoryInputModel
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(200)]
        [UIHint("Description")]
        public string Description { get; set; }
    }
}