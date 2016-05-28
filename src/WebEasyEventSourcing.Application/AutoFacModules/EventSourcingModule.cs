using Autofac;
using WebEasyEventSourcing.Application.Implementation;
using WebEasyEventSourcing.Data;
using WebEasyEventSourcing.EventSourcing.Handlers;
using WebEasyEventSourcing.EventSourcing.Persistence;

namespace WebEasyEventSourcing.Application.AutoFacModules
{
    public class EventSourcingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CommandDispatcher>().As<ICommandDispatcher>();
            builder.RegisterType<CommandHandlerFactory>().As<ICommandHandlerFactory>();
            builder.RegisterType<InMemoryEventStore>().As<IEventStore>().SingleInstance();
        }
    }
}