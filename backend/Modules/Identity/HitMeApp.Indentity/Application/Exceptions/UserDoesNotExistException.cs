using System;
using HitMeApp.Shared.Infrastructure.Exceptions;

namespace HitMeApp.Indentity.Application.Exceptions
{
    internal class UserDoesNotExistException : AppException
    {
        public override string Code => "user_does_not_exist";

        public Guid Guid { get; }

        public UserDoesNotExistException(Guid guid) : base($"The user with this guid does not exist: {guid}")
        {
            Guid = guid;
        }
    }
}
