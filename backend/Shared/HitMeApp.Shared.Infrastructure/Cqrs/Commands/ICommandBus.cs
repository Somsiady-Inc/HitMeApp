using System.Threading.Tasks;

namespace HitMeApp.Shared.Infrastructure.Cqrs.Commands
{
    public interface ICommandBus
    {
        public Task Dispatch<TCommand>(TCommand command) where TCommand : class, ICommand;
        public Task<TResult> Dispatch<TResult>(ICommand<TResult> command);
    }
}
