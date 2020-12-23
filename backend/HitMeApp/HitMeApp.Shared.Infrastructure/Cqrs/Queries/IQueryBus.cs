using System.Threading.Tasks;

namespace HitMeApp.Shared.Infrastructure.Cqrs.Queries
{
    public interface IQueryBus
    {
        public Task<TResult> Query<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>;
    }
}
