using Autofac;
using HitMeApp.Shared.Infrastructure.Cqrs.Commands;
using HitMeApp.Shared.Infrastructure.Cqrs.Queries;
using HitMeApp.Users.Contract;
using HitMeApp.Users.Contract.Commands;
using HitMeApp.Users.Contract.Queries;
using System.Threading.Tasks;

namespace HitMeApp.Users
{
    internal sealed class DefaultUserModule : IUserModule
    {
        public async Task Command<TCommand>(IUserCommand command)
        {
            using var scope = UserModuleCompositionRoot.BeginLifetimeScope();
            await scope.Resolve<ICommandBus>().Dispatch(command);
        }

        public async Task<TResult> Query<TResult>(IUserQuery<TResult> query)
        {
            using var scope = UserModuleCompositionRoot.BeginLifetimeScope();
            return await scope.Resolve<IQueryBus>().Query(query);
        }
    }
}
