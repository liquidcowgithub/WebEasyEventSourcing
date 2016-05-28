using WebEasyEventSourcing.Messages;

namespace WebEasyEventSourcing.EventSourcing.Handlers
{
    public interface IEventObserver
    {
        void Notify<TEvent>(TEvent evt) where TEvent : IEvent;
    }
}