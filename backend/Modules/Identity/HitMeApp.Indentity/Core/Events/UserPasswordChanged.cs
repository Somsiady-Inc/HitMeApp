using System;
using HitMeApp.Shared.DDD;

namespace HitMeApp.Indentity.Core.Events
{
    internal class UserPasswordChanged : DomainEvent
    {
        public Guid UserId { get; init; }

        public UserPasswordChanged(UserId userId)
        {
            UserId = userId;
        }
    }
}
