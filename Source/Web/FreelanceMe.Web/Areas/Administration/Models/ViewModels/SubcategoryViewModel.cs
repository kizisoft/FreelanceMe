namespace FreelanceMe.Web.Areas.Administration.Models.ViewModels
{
    using FreelanceMe.Data.Models;
    using FreelanceMe.Web.Infrastructure.Mapping;
    using System.ComponentModel.DataAnnotations;

    public class SubcategoryViewModel : IMapFrom<Subcategory>
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; }
    }
}