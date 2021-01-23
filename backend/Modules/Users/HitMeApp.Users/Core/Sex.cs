using System;
using System.Collections.Generic;
using System.Linq;
using HitMeApp.Shared.DDD;

namespace HitMeApp.Users.Core
{
    internal class Sex : ValueObject
    {
        private static readonly List<int> _validValues = typeof(Sex).GetProperties()
            .Where(prop =>
            {
                var getMethod = prop.GetGetMethod();
                return getMethod is { } && getMethod.IsStatic && getMethod.ReturnType == typeof(Sex);
            })
            .Select(prop => (prop.GetGetMethod().Invoke(null, Array.Empty<object>()) as Sex).Value)
            .ToList();

        public static Sex NotKnown => new Sex(0);
        public static Sex Male => new Sex(1);
        public static Sex Female => new Sex(2);
        public static Sex NotApplicable => new Sex(9);

        public int Value { get; }

        protected Sex(int value)
        {
            Value = value;
        }

        public static Sex From(int value)
        {
            if (!_validValues.Contains(value))
            {
                throw new ArgumentException($"Not a valid value for sex: {value}", nameof(value));
            }
            return new Sex(value);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static implicit operator int(Sex sex)
            => sex.Value;
    }
}
