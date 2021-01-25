using HitMeApp.Shared.DDD;

namespace HitMeApp.Shared.Infrastructure.Integration.EventProcessor
{
    public interface IEventMapper
    {
        public IntegrationEvent Map(DomainEvent @event);
    }
}
