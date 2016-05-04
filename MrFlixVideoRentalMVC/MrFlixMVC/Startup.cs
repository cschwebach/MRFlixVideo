using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MrFlixMVC.Startup))]
namespace MrFlixMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
