using System.Threading.Tasks;

namespace HitMeApp.Shared.Infrastructure.Cqrs.Commands
{
    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        public Task Handle(TCommand command);
    }

    public interface ICommandHandler<TCommand, TResult> where TCommand : ICommand
    {
        public Task<TResult> Handle(TCommand command);
    }
}
