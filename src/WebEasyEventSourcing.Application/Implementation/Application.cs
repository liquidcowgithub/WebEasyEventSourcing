using WebEasyEventSourcing.Data;
using WebEasyEventSourcing.Domain.ShoppingCart;
using WebEasyEventSourcing.EventSourcing.Handlers;
using WebEasyEventSourcing.Messages;

namespace WebEasyEventSourcing.Application.Implementation
{
    public class Application : IApplication
    {
        private readonly ICommandDispatcher commandDispatcher;
        private readonly IEventObserver eventObserver;

        public Application(IShoppingCartContext shoppingCartContext,
            ICommandDispatcher commandDispatcher,
            IEventObserver eventObserver)
        {
            this.eventObserver = eventObserver;
            ShoppingCartContext = shoppingCartContext;
            this.commandDispatcher = commandDispatcher;
        }

        public IShoppingCartContext ShoppingCartContext { get; set; }

        public void Send<TCommand>(TCommand cmd) where TCommand : ICommand
        {
            commandDispatcher.Send(cmd);
        }
    }
}