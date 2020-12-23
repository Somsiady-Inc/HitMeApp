using Autofac;
using System;
using System.Threading.Tasks;

namespace HitMeApp.Shared.Infrastructure.Cqrs.Commands
{
    internal sealed class InMemoryCommandBus : ICommandBus
    {
        private readonly IComponentContext _context;

        public InMemoryCommandBus(IComponentContext context)
        {
            _context = context;
        }

        public async Task Dispatch<TCommand>(TCommand command) where TCommand : ICommand
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(TCommand), "Command cannot be null");
            }

            var didResolveHandler = _context.TryResolve(out ICommandHandler<TCommand> handler);

            if(didResolveHandler)
            {
                await handler.Handle(command);
            }
        }

        public async Task<TResult> Dispatch<TCommand, TResult>(TCommand command) where TCommand : ICommand
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(TCommand), "Command cannot be null");
            }

            var handler = _context.Resolve<ICommandHandler<TCommand, TResult>>();
            return await handler.Handle(command);
        }
    }
}
