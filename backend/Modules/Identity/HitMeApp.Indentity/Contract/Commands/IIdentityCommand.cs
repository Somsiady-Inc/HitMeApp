using HitMeApp.Shared.Infrastructure.Cqrs.Commands;

namespace HitMeApp.Indentity.Contract.Commands
{
    public interface IIdentityCommand : ICommand
    {
        // Limiting interface
    }

    public interface IIdentityCommand<out TResult> : ICommand<TResult>
    {
        // Limiting interface
    }
}
