using WebEasyEventSourcing.Messages;

namespace WebEasyEventSourcing.EventSourcing.Handlers
{
    public interface IEventDispatcher
    {
        void Send<TEvent>(TEvent evt) where TEvent : IEvent;
    }
}