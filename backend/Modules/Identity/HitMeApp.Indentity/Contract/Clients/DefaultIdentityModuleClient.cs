using System.Threading.Tasks;
using Autofac;
using HitMeApp.Indentity.Contract.Commands;
using HitMeApp.Indentity.Contract.Queries;
using HitMeApp.Shared.Infrastructure.Cqrs.Commands;
using HitMeApp.Shared.Infrastructure.Cqrs.Queries;

namespace HitMeApp.Indentity.Contract.Clients
{
    internal sealed class DefaultIdentityModuleClient : IIdentityModuleClient
    {
        public async Task Command<TCommand>(TCommand command) where TCommand : class, IIdentityCommand
        {
            using var scope = IdentityModuleCompositionRoot.BeginLifetimeScope();
            await scope.Resolve<ICommandBus>().Dispatch(command);
        }

        public async Task<TResult> Command<TResult>(IIdentityCommand<TResult> command)
        {
            using var scope = IdentityModuleCompositionRoot.BeginLifetimeScope();
            return await scope.Resolve<ICommandBus>().Dispatch(command);
        }

        public async Task<TResult> Query<TResult>(IIdentityQuery<TResult> query)
        {
            using var scope = IdentityModuleCompositionRoot.BeginLifetimeScope();
            return await scope.Resolve<IQueryBus>().Query(query);
        }
    }
}
