namespace FreelanceMe.Web.ViewModels.Profile
{
    using System;
    using FreelanceMe.Data.Models;
    using FreelanceMe.Web.Infrastructure.Mapping;

    public class ProfileHomeViewModel : IMapFrom<UserProfile>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Avatar { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}