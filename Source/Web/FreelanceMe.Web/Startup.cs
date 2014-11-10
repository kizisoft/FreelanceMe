using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FreelanceMe.Web.Startup))]
namespace FreelanceMe.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
