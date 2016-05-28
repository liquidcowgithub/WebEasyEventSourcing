﻿using System;
using System.Collections.Generic;
using WebEasyEventSourcing.Application;
using WebEasyEventSourcing.Application.Implementation;
using WebEasyEventSourcing.EventSourcing;
using WebEasyEventSourcing.EventSourcing.Domain;
using WebEasyEventSourcing.EventSourcing.Handlers;
using WebEasyEventSourcing.EventSourcing.Persistence;
using WebEasyEventSourcing.Messages;
using NUnit.Framework;
using SharpTestsEx;

namespace WebEasyEventSourcing.Tests.Domain.Helpers
{
    class Spesification
    {
        protected readonly ICommandDispatcher app;
        protected readonly TestEventStore eventStore;

        protected Spesification()
        {
            this.eventStore = new TestEventStore();
            var commandHandlerFactory = new CommandHandlerFactory(this.eventStore);
            this.app = new CommandDispatcher(commandHandlerFactory);
        }

        protected void And<TEventStream, TEvent>(Guid id, TEvent evt)
            where TEvent : IEvent
            where TEventStream : EventStream, new()
        {
            this.Given<TEventStream, TEvent>(id, evt);
        }

        protected void Given<TEventStream, TEvent>(Guid id, TEvent evt) 
                                                    where TEvent : IEvent
                                                    where TEventStream : EventStream
        {
            var streamIdentifier = new StreamIdentifier(typeof(TEventStream).Name, id);
            var eventStoreStream = new EventStoreStream(streamIdentifier, new List<IEvent>{evt});
            this.eventStore.Save(new List<EventStoreStream>{eventStoreStream});
            this.eventStore.CommitInitialEvents();
        }

        protected void When<T>(T command) where T : ICommand
        {
            this.app.Send<T>(command);
        }

        protected void Then(params IEvent[] expectedEvents)
        {
            this.eventStore.NewEvents().Should()
                      .Not.Be.Empty()
                      .And.Have.SameSequenceAs(expectedEvents)
                      .And.Have.UniqueValues();
        }

        protected void ThenNoEvents()
        {
            this.eventStore.NewEvents().Should()
                .Be.Empty();
        }

        protected void ThrowsWhen<TException,T>(T command) where TException : Exception where T :ICommand
        {
            Assert.Throws<TException>(
                () => this.When(command)
            );
        }

        [TearDown]
        public void TearDown()
        {
            this.eventStore.Complete();
        }
    }
}
