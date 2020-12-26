using System;
using System.Threading.Tasks;
using HitMeApp.Shared.Infrastructure.Cqrs.Commands;
using HitMeApp.Users.Contract.Commands;

namespace HitMeApp.Users.Handlers.Commands
{
    internal sealed class RegisterUserHandler : ICommandHandler<RegisterUser>
    {
        // JWT generator
        // User repository

        public Task Handle(RegisterUser command)
        {
            // Business
            // Send integration event
            throw new NotImplementedException();
        }
    }
}
