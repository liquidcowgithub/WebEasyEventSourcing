using Autofac;

namespace WebEasyEventSourcing.Application.AutoFacModules
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Implementation.Application>().As<IApplication>().SingleInstance();
        }
    }
}