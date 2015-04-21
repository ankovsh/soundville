using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Soundville.Configuration.WindsorCastle;
using Soundville.Configuration.WindsorCastle.Plumbing;
using Soundville.Infrastructure.WindsorCastle;
using Soundville.Presentation.Streaming;

namespace Soundville.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            WindsorCastleConfig.RegisterInstallers();

            GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerActivator), new WindsorCompositionRoot(IoC.ContainerInstance));
            GlobalConfiguration.Configure(WebApiConfig.Register);

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Mp3StreamingPool.Instance.CheckAddBinPath();
        }
    }
}
