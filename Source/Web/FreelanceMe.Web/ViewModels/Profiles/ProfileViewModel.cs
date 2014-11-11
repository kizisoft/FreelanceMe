namespace FreelanceMe.Web.ViewModels.Profiles
{
    using AutoMapper;
    using System.ComponentModel.DataAnnotations;

    using FreelanceMe.Data.Models;
    using FreelanceMe.Web.Infrastructure.Mapping;

    public class ProfileViewModel : IMapFrom<UserProfile>
    {
        public virtual ApplicationUser User { get; set; }

        [Required]
        [StringLength(30)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30)]
        public string LastName { get; set; }

        [Required]
        [StringLength(50)]
        public string Country { get; set; }

        [Required]
        [StringLength(30)]
        public string City { get; set; }
    }
}