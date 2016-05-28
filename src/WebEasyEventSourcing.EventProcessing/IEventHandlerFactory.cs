using System.Collections.Generic;
using WebEasyEventSourcing.EventSourcing.Handlers;
using WebEasyEventSourcing.Messages;

namespace WebEasyEventSourcing.EventProcessing
{
    public interface IEventHandlerFactory
    {
        IEnumerable<IEventHandler<TEvent>> Resolve<TEvent>(TEvent evt) where TEvent : IEvent;
    }
}