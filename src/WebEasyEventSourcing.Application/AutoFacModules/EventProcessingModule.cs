using Autofac;
using WebEasyEventSourcing.EventProcessing;
using WebEasyEventSourcing.EventSourcing.Handlers;

namespace WebEasyEventSourcing.Application.AutoFacModules
{
    public class EventProcessingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EventDispatcher>().As<IEventDispatcher>();
            builder.RegisterType<EventHandlerFactory>().As<IEventHandlerFactory>();
            builder.RegisterType<EventDispatcher>().As<IEventDispatcher>();
            builder.RegisterType<EventProcessor>().As<IEventObserver>();
        }
    }
}