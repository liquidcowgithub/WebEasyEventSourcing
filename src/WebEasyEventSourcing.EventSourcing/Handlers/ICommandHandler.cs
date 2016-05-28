using WebEasyEventSourcing.Messages;

namespace WebEasyEventSourcing.EventSourcing.Handlers
{
    public interface ICommandHandler<in TCommand> : IHandler where TCommand : ICommand
    {
        void Handle(TCommand cmd);
    }
}