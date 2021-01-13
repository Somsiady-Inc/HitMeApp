using System;
using System.Collections.Generic;
using HitMeApp.Shared.DDD;
using HitMeApp.Users.Core.Specification;

namespace HitMeApp.Users.Core
{
    internal class PersonalInfo : ValueObject
    {
        private readonly IMinimalAgeSpecification _minimalAgeSpecification;

        public string Nickname { get; }
        public string Description { get; }
        public DateTime? BirthDate { get; }
        public Sex Sex { get; }

        public PersonalInfo(string nickname,
                            string description,
                            DateTime? birthDate,
                            Sex sex,
                            IMinimalAgeSpecification minimalAgeSpecification)
        {
            Nickname = nickname;
            Description = description;
            BirthDate = birthDate;
            Sex = sex;
            _minimalAgeSpecification = minimalAgeSpecification;
        }

        public PersonalInfo WithNickname(string nickname)
            => new PersonalInfo(nickname, Description, BirthDate, Sex, _minimalAgeSpecification);

        public PersonalInfo WithDescription(string description)
            => new PersonalInfo(Nickname, description, BirthDate, Sex, _minimalAgeSpecification);

        public PersonalInfo WithBirthDate(DateTime birthDate)
            => new PersonalInfo(Nickname, Description, birthDate, Sex, _minimalAgeSpecification);

        public PersonalInfo WithSex(Sex sex)
            => new PersonalInfo(Nickname, Description, BirthDate, sex, _minimalAgeSpecification);

        public bool Valid => string.IsNullOrWhiteSpace(Nickname) &&
            Nickname.Length > 3 &&
            _minimalAgeSpecification.IsSatisfiedBy(this) &&
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
