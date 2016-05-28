using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using WebEasyEventSourcing.Application;
using Owin;

namespace WebEasyEventSourcing.Web.Api.App_Start
{
    public class DependencyInjectionConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var builder = Bootstrapper.InitialiseContainerBuilder();

            //if (Config.AuthenticationMethod.Equals("Windows"))
            //{
            //    builder.RegisterModule(new WindowsAuthenticationModule());
            //}
            //else
            //{
            //    builder.RegisterModule(new StandardAuthenticationModule());
            //}

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()).InstancePerRequest();
            builder.RegisterWebApiFilterProvider(config);

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            //app.UseAutofacMiddleware(container);
            //app.UseAutofacWebApi(config);

            //return container;
        }
    }
}