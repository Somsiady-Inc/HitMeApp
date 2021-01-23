using HitMeApp.Shared.DDD;

namespace HitMeApp.Users.Core.Events
{
    internal class UserRemovedTrait : DomainEvent
    {
        public UserId UserId { get; }
        public Trait Trait { get; }

        public UserRemovedTrait(UserId userId, Trait trait)
        {
            UserId = userId;
            Trait = trait;
        }
    }
}
