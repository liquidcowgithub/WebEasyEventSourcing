using System.Reflection;
using Autofac;
using Autofac.Integration.WebApi;
using WebEasyEventSourcing.Application.AutoFacModules;
using WebEasyEventSourcing.Application.Implementation;
using WebEasyEventSourcing.Data;
using WebEasyEventSourcing.Data.DomainContexts;
using WebEasyEventSourcing.EventProcessing;

namespace WebEasyEventSourcing.Application
{
    public class Bootstrapper
    {
        public static Implementation.Application Bootstrap()
        {
            var store = new InMemoryEventStore();
            var handlerFactory = new CommandHandlerFactory(store);
            var dispatcher = new CommandDispatcher(handlerFactory);
            
            var shoppingCartContext = new ShoppingCartContext();
            var eventHandlerFactory = new EventHandlerFactory(store, dispatcher, shoppingCartContext);
            var eventDispatcher = new EventDispatcher(eventHandlerFactory);
            var eventProcessor = new EventProcessor(store, eventDispatcher);

            return new Implementation.Application(shoppingCartContext, dispatcher, eventProcessor);
        }

        public static ContainerBuilder InitialiseContainerBuilder()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new ApplicationModule());
            builder.RegisterModule(new ContextsModule());
            builder.RegisterModule(new EventSourcingModule());
            builder.RegisterModule(new EventProcessingModule());
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            return builder;
        }

        
    }
}
