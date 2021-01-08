using HitMeApp.Shared.DDD;

namespace HitMeApp.Users.Core.Events
{
    internal class UserAddedTrait : DomainEvent
    {
        public UserId UserId { get; }
        public Trait Trait { get; }

        public UserAddedTrait(UserId userId, Trait trait)
        {
            UserId = userId;
            Trait = trait;
        }
    }
}
