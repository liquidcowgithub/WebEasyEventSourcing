using Autofac;
using WebEasyEventSourcing.EventSourcing.Persistence;

namespace WebEasyEventSourcing.Application.AutoFacModules
{
    public class RepositoriesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Repository>().As<IRepository>();
        }
    }
}