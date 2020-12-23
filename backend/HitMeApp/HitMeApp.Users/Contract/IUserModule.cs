using HitMeApp.Users.Contract.Commands;
using HitMeApp.Users.Contract.Queries;
using System.Threading.Tasks;

namespace HitMeApp.Users.Contract
{
    public interface IUserModule
    {
        public Task Command<TCommand>(IUserCommand command);
        public Task<TResult> Query<TResult>(IUserQuery<TResult> query);
    }
}
