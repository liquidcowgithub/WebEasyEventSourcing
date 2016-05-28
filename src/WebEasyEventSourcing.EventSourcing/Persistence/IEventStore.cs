using System;
using System.Collections.Generic;
using WebEasyEventSourcing.EventSourcing.Domain;
using WebEasyEventSourcing.EventSourcing.Handlers;
using WebEasyEventSourcing.Messages;

namespace WebEasyEventSourcing.EventSourcing.Persistence
{
    public interface IEventStore
    {
        IEnumerable<IEvent> GetByStreamId(StreamIdentifier streamId);
        void Save(List<EventStoreStream> newEvents);
        Action Subscribe(IEventObserver observer);
    }
}