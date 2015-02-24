using Microsoft.Owin;
using Owin;
using Soundville.Web;

[assembly: OwinStartup(typeof(Startup))]
namespace Soundville.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}