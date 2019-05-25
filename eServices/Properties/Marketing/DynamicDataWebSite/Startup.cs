using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DynamicDataWebSite.Startup))]
namespace DynamicDataWebSite
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
