﻿using System.Threading.Tasks;

namespace HitMeApp.Shared.Infrastructure.Cqrs.Commands
{
    public interface ICommandHandler<TCommand> where TCommand : class, ICommand
    {
        public Task Handle(TCommand command);
    }

    public interface ICommandHandler<in TCommand, TResult> where TCommand : class, ICommand<TResult>
    {
        public Task<TResult> Handle(TCommand command);
    }
}
