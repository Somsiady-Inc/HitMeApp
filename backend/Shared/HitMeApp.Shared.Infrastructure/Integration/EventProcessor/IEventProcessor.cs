using System.Collections.Generic;
using System.Threading.Tasks;
using HitMeApp.Shared.DDD;

namespace HitMeApp.Shared.Infrastructure.Integration.EventProcessor
{
    public interface IEventProcessor
    {
        public Task Process(IEnumerable<DomainEvent> events);
    }
}
