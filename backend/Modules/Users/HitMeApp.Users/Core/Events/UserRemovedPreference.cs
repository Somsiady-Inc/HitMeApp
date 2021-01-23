using HitMeApp.Shared.DDD;

namespace HitMeApp.Users.Core.Events
{
    internal class UserRemovedPreference : DomainEvent
    {
        public UserId UserId { get; }
        public Trait Preference { get; }

        public UserRemovedPreference(UserId userId, Trait preference)
        {
            UserId = userId;
            Preference = preference;
        }
    }
}
