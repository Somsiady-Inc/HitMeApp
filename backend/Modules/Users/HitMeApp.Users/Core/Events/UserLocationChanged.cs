using HitMeApp.Shared.DDD;

namespace HitMeApp.Users.Core.Events
{
    internal class UserLocationChanged : DomainEvent
    {
        public UserId UserId { get; }
        public Location Location { get; }

        public UserLocationChanged(UserId userId, Location location)
        {
            UserId = userId;
            Location = location;
        }
    }
}
