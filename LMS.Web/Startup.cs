using LMS.Web.App_Start;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LMS.Web.Startup))]
namespace LMS.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var container = AutofacConfig.RegisterDependancies(app);

            DatabaseConfig.Initialize(container);

            ConfigureAuth(app);
        }
    }
}
