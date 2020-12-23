using System.Threading.Tasks;

namespace HitMeApp.Shared.Infrastructure.Cqrs.Commands
{
    public interface ICommandBus
    {
        public Task Dispatch<TCommand>(TCommand command) where TCommand : ICommand;
        public Task<TResult> Dispatch<TCommand, TResult>(TCommand command) where TCommand : ICommand;
    }
}
