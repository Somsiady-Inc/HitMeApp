using System.Threading.Tasks;
using HitMeApp.Indentity.Contract.Commands;
using HitMeApp.Indentity.Contract.Queries;

namespace HitMeApp.Indentity.Contract.Clients
{
    public interface IIdentityModuleClient
    {
        public Task Command<TCommand>(TCommand command) where TCommand : class, IIdentityCommand;
        public Task<TResult> Command<TResult>(IIdentityCommand<TResult> command);
        public Task<TResult> Query<TResult>(IIdentityQuery<TResult> query);
    }
}
