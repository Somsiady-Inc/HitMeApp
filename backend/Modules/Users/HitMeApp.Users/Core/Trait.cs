using System;
using HitMeApp.Shared.DDD;
using HitMeApp.Users.Core.Exceptions;

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
        private static readonly int _maxTraitLength = 20;

        public string Value { get; }

        protected Trait(TraitId id, string value) : base(id)
        {
            Value = value;
        }

        public static Trait New(string value)
        {
            var trimmedValue = value?.Trim();
            if (string.IsNullOrWhiteSpace(trimmedValue) || trimmedValue.Length > _maxTraitLength)
            {
                throw new InvalidTraitException(trimmedValue, _maxTraitLength);
            }
            return new Trait(new TraitId(Guid.NewGuid()), trimmedValue.ToLowerInvariant());
        }

        public static Trait Load(Guid id, string value)
            => new Trait(new TraitId(id), value);
    }
}
