using WebEasyEventSourcing.Messages;

namespace WebEasyEventSourcing.Tests.Domain.Helpers
{
    public class EventStreamEvent
    {
        public EventStreamEvent(IEvent evt)
        {
            this.Event = evt;
            this.IsCommitted = false;
        }

        public IEvent Event { get; private set; }
        public bool IsCommitted { get; private set; }
        public void Commit()
        {
            this.IsCommitted = true;
        }
    }
}
