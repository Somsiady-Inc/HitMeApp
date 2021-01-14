using System.Threading.Tasks;
using Autofac;
using HitMeApp.Shared.Infrastructure.Cqrs.Commands;
using HitMeApp.Shared.Infrastructure.Cqrs.Queries;
using HitMeApp.Users.Contract.Commands;
using HitMeApp.Users.Contract.Queries;

namespace HitMeApp.Users.Contract.Clients
{
    internal sealed class DefaultUserModuleClient : IUserModuleClient
    {
        public async Task Command<TCommand>(TCommand command) where TCommand : class, IUserCommand
        {
            using var scope = UserModuleCompositionRoot.BeginLifetimeScope();
            await scope.Resolve<ICommandBus>().Dispatch(command);
        }

        public async Task<TResult> Command<TResult>(IUserCommand<TResult> command)
        {
            using var scope = UserModuleCompositionRoot.BeginLifetimeScope();
            return await scope.Resolve<ICommandBus>().Dispatch(command);
        }

        public async Task<TResult> Query<TResult>(IUserQuery<TResult> query)
        {
            using var scope = UserModuleCompositionRoot.BeginLifetimeScope();
            return await scope.Resolve<IQueryBus>().Query(query);
        }
    }
}
