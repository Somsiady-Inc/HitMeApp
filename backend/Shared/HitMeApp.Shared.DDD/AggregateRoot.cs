using System.Collections.Generic;

namespace HitMeApp.Shared.DDD
{
    public abstract class AggregateRoot<TEntityId> : Entity<TEntityId> where TEntityId : EntityId
    {
        private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();

        public int Version { get; protected set; }
        public IEnumerable<IDomainEvent> DomainEvents => _domainEvents;

        protected AggregateRoot(TEntityId id) : base(id)
        {
        }

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
