using System;
using System.Collections.Generic;
using System.Linq;
using WebEasyEventSourcing.Domain.Shipping;
using WebEasyEventSourcing.Domain.ShoppingCart;
using WebEasyEventSourcing.EventSourcing.Handlers;
using WebEasyEventSourcing.EventSourcing.Persistence;
using WebEasyEventSourcing.Messages;
using WebEasyEventSourcing.Messages.Orders;
using WebEasyEventSourcing.Messages.Store;

namespace WebEasyEventSourcing.EventProcessing
{
    public class EventHandlerFactory : IEventHandlerFactory
    {
        private readonly Dictionary<Type, List<Func<IHandler>>> handlerFactories =
            new Dictionary<Type, List<Func<IHandler>>>();

        public EventHandlerFactory(IEventStore eventStore, 
            ICommandDispatcher dispatcher,
            IShoppingCartContext shoppingCartContext)
        {
            RegisterHandlerFactories(eventStore, dispatcher, shoppingCartContext);
        }

        public IEnumerable<IEventHandler<TEvent>> Resolve<TEvent>(TEvent evt) where TEvent : IEvent
        {
            var evtType = evt.GetType();
            if (handlerFactories.ContainsKey(evtType))
            {
                var factories = handlerFactories[evtType];
                return factories.Select(h => (IEventHandler<TEvent>) h());
            }
            return new List<IEventHandler<TEvent>>();
        }

        private void RegisterHandlerFactories(IEventStore eventStore, ICommandDispatcher dispatcher,
            IShoppingCartContext shoppingCartContext)
        {
            RegisterHandlerFactoryWithTypes(
                () => new ShoppingCartEventHandler(shoppingCartContext),
                typeof(CartCreated),
                typeof(ProductAddedToCart),
                typeof(ProductRemovedFromCart),
                typeof(CartEmptied),
                typeof(CartCheckedOut));

            RegisterHandlerFactoryWithTypes(
                () => new OrderEventHandler(new Repository(eventStore), dispatcher),
                typeof(OrderCreated),
                typeof(PaymentReceived),
                typeof(ShippingAddressConfirmed));
        }

        private void RegisterHandlerFactoryWithTypes(Func<IHandler> handler, params Type[] types)
        {
            foreach (var type in types)
            {
                handlerFactories.Add(type, new List<Func<IHandler>> {handler});
            }
        }
    }
}