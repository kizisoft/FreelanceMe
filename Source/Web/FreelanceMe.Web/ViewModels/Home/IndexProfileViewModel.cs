namespace FreelanceMe.Web.ViewModels.Home
{
    using FreelanceMe.Data.Models;
    using FreelanceMe.Web.Infrastructure.Mapping;

    public class IndexProfileViewModel : IMapFrom<Profile>
    {
        public ApplicationUser User { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Rating { get; set; }
    }
}