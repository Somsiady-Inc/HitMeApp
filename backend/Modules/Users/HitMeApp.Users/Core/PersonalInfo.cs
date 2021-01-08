using System;
using System.Collections.Generic;
using HitMeApp.Shared.DDD;

namespace HitMeApp.Users.Core
{
    internal class PersonalInfo : ValueObject
    {
        public string Nickname { get; }
        public string Description { get; }
        public DateTime BirthDate { get; }
        public Sex Sex { get; }

        public bool Valid => string.IsNullOrWhiteSpace(Nickname) &&
            Nickname.Length > 3 &&
            DateTime.Now >= BirthDate.AddYears(18) &&
            Sex != Sex.NotKnown && Sex != Sex.NotApplicable;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Nickname;
            yield return Description;
            yield return BirthDate;
            yield return Sex;
        }
    }
}
