using System;
using WebEasyEventSourcing.Data;
using WebEasyEventSourcing.EventSourcing.Handlers;
using WebEasyEventSourcing.EventSourcing.Persistence;
using WebEasyEventSourcing.Messages;

namespace WebEasyEventSourcing.EventProcessing
{
    public class EventProcessor : IEventObserver
    {
        private readonly IEventDispatcher dispatcher;
        private Action unsubscribe;

        public EventProcessor(IEventStore store, IEventDispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
            this.unsubscribe = store.Subscribe(this);
        }

        public void Notify<TEvent>(TEvent evt) where TEvent : IEvent
        {
            dispatcher.Send(evt);
        }
    }
}
