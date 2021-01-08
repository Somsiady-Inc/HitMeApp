using HitMeApp.Shared.DDD;

namespace HitMeApp.Users.Core.Events
{
    internal class UserAddedPreference : DomainEvent
    {
        public UserId UserId { get; }
        public Trait Preference { get; }

        public UserAddedPreference(UserId userId, Trait preference)
        {
            UserId = userId;
            Preference = preference;
        }
    }
}
