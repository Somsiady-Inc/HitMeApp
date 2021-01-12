using System;
using HitMeApp.Shared.Infrastructure.Exceptions;

namespace HitMeApp.Users.Application.Exceptions
{
    internal sealed class UserAlreadyExistsException : AppException
    {
        public override string Code => "user_already_exists_exception";

        public Guid Id { get; }

        public UserAlreadyExistsException(Guid id) : base($"User with this ID already exists: {id}")
        {
            Id = id;
        }
    }
}
