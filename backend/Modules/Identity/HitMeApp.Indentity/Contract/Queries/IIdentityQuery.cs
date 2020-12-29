using HitMeApp.Shared.Infrastructure.Cqrs.Queries;

namespace HitMeApp.Indentity.Contract.Queries
{
    public interface IIdentityQuery<out TResult> : IQuery<TResult>
    {
        // Limiting interface
    }
}
