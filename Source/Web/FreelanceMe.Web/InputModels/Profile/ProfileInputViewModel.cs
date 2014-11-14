namespace FreelanceMe.Web.InputModels.Profile
{
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;

    using FreelanceMe.Data.Models;
    using FreelanceMe.Web.Infrastructure.Mapping;

    public class ProfileInputViewModel : IMapFrom<UserProfile>
    {
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

        public string Avatar { get; set; }
    }
}