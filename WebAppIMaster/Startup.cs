using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebAppWebAppIMaster.Startup))]
namespace WebAppWebAppIMaster
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
