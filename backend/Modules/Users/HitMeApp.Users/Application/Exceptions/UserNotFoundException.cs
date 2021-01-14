using System;
using HitMeApp.Shared.Infrastructure.Exceptions;

namespace HitMeApp.Users.Application.Exceptions
{
    internal class UserNotFoundException : AppException
    {
        public override string Code => "user_not_found";

        public Guid Id { get; }

        public UserNotFoundException(Guid id) : base($"The user with a given id has not been found: {id}")
        {
            Id = id;
        }
    }
}
