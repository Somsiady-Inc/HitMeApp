using System;
using HitMeApp.Shared.DDD;

namespace HitMeApp.Indentity.Core.Events
{
    internal class UserPasswordChanged : IDomainEvent
    {
        public Guid Id { get; init; }

        public UserPasswordChanged(UserId id)
        {
            Id = id;
        }
    }
}
