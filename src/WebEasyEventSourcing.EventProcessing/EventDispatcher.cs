﻿using WebEasyEventSourcing.EventSourcing.Handlers;
using WebEasyEventSourcing.Messages;

namespace WebEasyEventSourcing.EventProcessing
{
    public class EventDispatcher : IEventDispatcher
    {
        private readonly IEventHandlerFactory factory;

        public EventDispatcher(IEventHandlerFactory factory)
        {
            this.factory = factory;
        }
        public void Send<TEvent>(TEvent evt) where TEvent : IEvent
        {
            var handlers = this.factory.Resolve(evt);
            foreach (var eventHandler in handlers)
            {
                eventHandler.Handle(evt);
            }
        }
    }
}