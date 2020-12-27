using System;
using HitMeApp.Shared.DDD.Exceptions;

namespace HitMeApp.Shared.DDD
{
    public abstract class EntityId : IEquatable<EntityId>
    {
        public Guid Value { get; protected init; }

        public EntityId()
        {
            Value = Guid.NewGuid();
        }

        public EntityId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new InvalidAggregateIdException();
            }

            Value = value;
        }

        public bool Equals(EntityId other)
        {
            if (other is null) return false;
            return ReferenceEquals(this, other) || Value.Equals(other.Value);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((EntityId)obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static implicit operator Guid(EntityId id)
            => id.Value;

        public override string ToString() => Value.ToString();
    }
}
