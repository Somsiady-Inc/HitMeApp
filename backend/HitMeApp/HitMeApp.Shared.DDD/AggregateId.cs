using HitMeApp.Shared.DDD.Exceptions;
using System;

namespace HitMeApp.Shared.DDD
{
    public class AggregateId : IEquatable<AggregateId>
    {
        public Guid Value { get; protected init; }

        public AggregateId()
        {
            Value = Guid.NewGuid();
        }

        public AggregateId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new InvalidAggregateIdException();
            }

            Value = value;
        }

        public bool Equals(AggregateId other)
        {
            if (other is null) return false;
            return ReferenceEquals(this, other) || Value.Equals(other.Value);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((AggregateId)obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static implicit operator Guid(AggregateId id)
            => id.Value;

        public static implicit operator AggregateId(Guid id)
            => new AggregateId(id);

        public override string ToString() => Value.ToString();
    }
}
