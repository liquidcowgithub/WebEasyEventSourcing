﻿using System;
using WebEasyEventSourcing.EventSourcing.Domain;
using WebEasyEventSourcing.Messages;

namespace WebEasyEventSourcing.EventSourcing.Exceptions
{
    [Serializable]
    public class NoEventApplyMethodRegisteredException : EventSourceException
    {
        public NoEventApplyMethodRegisteredException(IEvent evt, EventStream eventStream) 
            : base (string.Format("No Event Applier Registered For {0} on {1}", evt.GetType().Name, eventStream.Name))
        {
        }
    }
}