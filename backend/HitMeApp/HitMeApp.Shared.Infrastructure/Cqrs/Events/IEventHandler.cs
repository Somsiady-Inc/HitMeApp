using System.Threading.Tasks;

namespace HitMeApp.Shared.Infrastructure.Cqrs.Events
{
    public interface IEventHandler<TEvent> where TEvent : IEvent
    {
        public Task Handle(TEvent @event);
    }
}
