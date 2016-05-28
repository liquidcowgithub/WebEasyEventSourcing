using WebEasyEventSourcing.EventSourcing.Handlers;
using WebEasyEventSourcing.Messages;

namespace WebEasyEventSourcing.Application.Implementation
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly ICommandHandlerFactory factory;

        public CommandDispatcher(ICommandHandlerFactory factory)
        {
            this.factory = factory;
        }

        public void Send<TCommand>(TCommand command) where TCommand : ICommand
        {
            var handler = this.factory.Resolve<TCommand>();
            handler.Handle(command);
        }
    }
}