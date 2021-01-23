using System;

namespace HitMeApp.Shared.DDD
{
    public abstract class DomainEvent
    {
        public Guid Id { get; }

        protected DomainEvent() : this(Guid.NewGuid())
        {
        }

        protected DomainEvent(Guid id)
        {
            Id = id;
        }
    }
}
