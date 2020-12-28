using System.Threading.Tasks;
using HitMeApp.Users.Contract.Commands;
using HitMeApp.Users.Contract.Queries;

namespace HitMeApp.Users.Contract
{
    public interface IUserModuleClient
    {
        public Task Command<TCommand>(TCommand command) where TCommand : class, IUserCommand;
        public Task<TResult> Query<TResult>(IUserQuery<TResult> query);
    }
}
