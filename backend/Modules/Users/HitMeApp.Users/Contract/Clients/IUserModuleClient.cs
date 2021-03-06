﻿using System.Threading.Tasks;
using HitMeApp.Users.Contract.Commands;
using HitMeApp.Users.Contract.Queries;

namespace HitMeApp.Users.Contract.Clients
{
    public interface IUserModuleClient
    {
        public Task Command<TCommand>(TCommand command) where TCommand : class, IUserCommand;
        public Task<TResult> Command<TResult>(IUserCommand<TResult> command);
        public Task<TResult> Query<TResult>(IUserQuery<TResult> query);
    }
}
