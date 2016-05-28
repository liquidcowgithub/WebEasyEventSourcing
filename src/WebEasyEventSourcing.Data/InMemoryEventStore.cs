﻿using System;
using System.Collections.Generic;
using WebEasyEventSourcing.EventSourcing.Domain;
using WebEasyEventSourcing.EventSourcing.Exceptions;
using WebEasyEventSourcing.EventSourcing.Handlers;
using WebEasyEventSourcing.EventSourcing.Persistence;
using WebEasyEventSourcing.Messages;

namespace WebEasyEventSourcing.Data
{
    public class InMemoryEventStore : IEventStore
    {
        private readonly Dictionary<string, List<IEvent>> store = new Dictionary<string, List<IEvent>>();

        private readonly List<IEventObserver> eventObservers = new List<IEventObserver>(); 

        public IEnumerable<IEvent> GetByStreamId(StreamIdentifier streamId)
        {
            if (store.ContainsKey(streamId.Value))
            {
                return store[streamId.Value].AsReadOnly();
            }
            throw new EventStreamNotFoundException(streamId);
        }

        public void Save(List<EventStoreStream> newEvents)
        {
            foreach (var eventStoreStream in newEvents)
            {
                this.PersistEvents(eventStoreStream);
                this.DispatchEvents(eventStoreStream.Events);
            }
        }

        private void PersistEvents(EventStoreStream eventStoreStream)
        {
            if(store.ContainsKey(eventStoreStream.Id))
            {
                store[eventStoreStream.Id].AddRange(eventStoreStream.Events);
            }
            else
            {
                store.Add(eventStoreStream.Id, eventStoreStream.Events);
            }
        }

        private void DispatchEvents(IEnumerable<IEvent> newEvents)
        {
            foreach (var evt in newEvents)
            {
                NotifySubscribers(evt);
            }
        }

        private void NotifySubscribers(IEvent evt)
        {
            dynamic typeAwareEvent = evt; //this cast is required to pass the correct Type to the Notify Method. Otherwise IEvent is used as the Type
            foreach(var observer in eventObservers)
            {
                observer.Notify(typeAwareEvent);
            }
        }

        public Action Subscribe(IEventObserver observer)
        {
            this.eventObservers.Add(observer);
            return () => this.Unsubscribe(observer);
        }

        private void Unsubscribe(IEventObserver observer)
        {
            eventObservers.Remove(observer);
        }
    }
}
