using System.Threading.Tasks;

namespace HitMeApp.Shared.Infrastructure.Cqrs.Queries
{
    public interface IQueryBus
    {
        public Task<TResult> Query<TResult>(IQuery<TResult> query);
    }
}
