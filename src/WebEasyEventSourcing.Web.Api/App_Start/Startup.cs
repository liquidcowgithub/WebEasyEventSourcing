//using System.Web.Http;
//using WebEasyEventSourcing.Web.Api.App_Start;
//using Microsoft.Owin;
//using Owin;

//[assembly: OwinStartup(typeof(Startup))]

//namespace WebEasyEventSourcing.Web.Api.App_Start
//{
//    public class Startup
//    {
//        public static HttpConfiguration HttpConfiguration { get; private set; }

//        public void Configuration(IAppBuilder app)
//        {
//            HttpConfiguration = new HttpConfiguration();
//            SwaggerConfig.Register(HttpConfiguration);
//            var container = DependencyInjectionConfig.Register(app, HttpConfiguration);
//            WebApiConfig.Register(HttpConfiguration);
//            //HangfireConfig.Register(app);
//            //AuthenticationConfig.Register(app, container);

//            app.UseWebApi(HttpConfiguration);
//        }
//    }
//}