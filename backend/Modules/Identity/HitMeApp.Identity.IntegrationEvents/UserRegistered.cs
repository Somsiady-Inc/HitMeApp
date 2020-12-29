using System;
using HitMeApp.Shared.Infrastructure.Integration;

namespace HitMeApp.Identity.IntegrationEvents
{
    public class UserRegistered : IntegrationEvent
    {
        public Guid UserId { get; }

        public UserRegistered(Guid userId)
        {
            UserId = userId;
        }
    }
}
