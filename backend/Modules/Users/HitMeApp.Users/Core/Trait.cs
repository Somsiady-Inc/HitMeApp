using System;
using HitMeApp.Shared.DDD;

namespace HitMeApp.Users.Core
{
    internal class TraitId : EntityId
    {
        public TraitId(Guid value) : base(value)
        {
        }
    }

    internal class Trait : Entity<TraitId>
    {
        public string Value { get; }

        protected Trait(TraitId id, string value) : base(id)
        {
            Value = value;
        }

        public static Trait New(string value)
            => new Trait(new TraitId(Guid.NewGuid()), value);

        public static Trait Load(Guid id, string value)
            => new Trait(new TraitId(id), value);
    }
}
