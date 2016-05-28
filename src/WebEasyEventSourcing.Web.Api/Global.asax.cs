using System.Web;
using System.Web.Http;
using System.Web.Routing;
using WebEasyEventSourcing.Web.Api.App_Start;

namespace WebEasyEventSourcing.Web.Api
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configure(SwaggerConfig.Register);
            GlobalConfiguration.Configure(DependencyInjectionConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}