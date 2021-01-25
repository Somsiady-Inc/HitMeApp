using System.Threading.Tasks;
using HitMeApp.Shared.DDD;

namespace HitMeApp.Shared.Infrastructure.Integration
{
    public interface IDomainEventHandler<in TDomainEvent> where TDomainEvent : DomainEvent
    {
        public Task Handle(TDomainEvent @event);
    }
}
