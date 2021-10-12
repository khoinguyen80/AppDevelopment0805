using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AppDevelopment0805.Startup))]
namespace AppDevelopment0805
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
