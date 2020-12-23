using System;

namespace HitMeApp.Shared.DDD
{
    public interface IDomainEvent
    {
        public Guid Id { get; }
    }
}
