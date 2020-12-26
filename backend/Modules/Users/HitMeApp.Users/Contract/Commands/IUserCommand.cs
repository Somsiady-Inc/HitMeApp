using HitMeApp.Shared.Infrastructure.Cqrs.Commands;

namespace HitMeApp.Users.Contract.Commands
{
    public interface IUserCommand : ICommand
    {
        // Limiting interface
    }

    public interface IUserCommand<out TResult> : ICommand<TResult>
    {
        // Limiting interface
    }
}
