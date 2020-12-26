using HitMeApp.Shared.Infrastructure.Cqrs.Queries;

namespace HitMeApp.Users.Contract.Queries
{
    public interface IUserQuery<out TResult> : IQuery<TResult>
    {
        // Limiting interface
    }
}
