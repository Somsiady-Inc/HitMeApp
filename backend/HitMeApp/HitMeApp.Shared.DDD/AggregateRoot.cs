using System.Collections.Generic;

namespace HitMeApp.Shared.DDD
{
    public abstract class AggregateRoot
    {
        private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();

        public AggregateId Id { get; protected init; }
        public int Version { get; protected set; }
        public IEnumerable<IDomainEvent> DomainEvents => _domainEvents;

        protected void RaiseDomainEvent(IDomainEvent @event)
        {
            _domainEvents.Add(@event);
        }

        public void ClearEvents()
        {
            _domainEvents.Clear();
        }
    }
}
