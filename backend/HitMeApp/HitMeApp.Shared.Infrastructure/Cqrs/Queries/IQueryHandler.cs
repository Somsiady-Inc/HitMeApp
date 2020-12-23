using System.Threading.Tasks;

namespace HitMeApp.Shared.Infrastructure.Cqrs.Queries
{
    public interface IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        public Task<TResult> Handle(TQuery query);
    }
}
