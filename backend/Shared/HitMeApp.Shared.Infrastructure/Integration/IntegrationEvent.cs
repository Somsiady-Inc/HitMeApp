using System;

namespace HitMeApp.Shared.Infrastructure.Integration
{
    public abstract class IntegrationEvent
    {
        public Guid Id { get; }
        public DateTime IssuedAt { get; }

        protected IntegrationEvent()
        {
            Id = Guid.NewGuid();
            IssuedAt = DateTime.UtcNow;
        }

        protected IntegrationEvent(Guid id, DateTime issuedAt)
        {
            Id = id;
            IssuedAt = issuedAt;
        }
    }
}
