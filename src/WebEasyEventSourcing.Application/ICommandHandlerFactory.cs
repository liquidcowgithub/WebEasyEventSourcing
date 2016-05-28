using WebEasyEventSourcing.EventSourcing.Handlers;
using WebEasyEventSourcing.Messages;

namespace WebEasyEventSourcing.Application
{
    public interface ICommandHandlerFactory
    {
        ICommandHandler<TCommand> Resolve<TCommand>() where TCommand : ICommand;
    }
}