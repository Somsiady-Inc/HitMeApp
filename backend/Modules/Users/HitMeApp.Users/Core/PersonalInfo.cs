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

        protected PersonalInfo(string nickname,
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

        public static PersonalInfo Empty(IMinimalAgeSpecification minimalAgeSpecification)
            => new PersonalInfo(null, null, null, null, minimalAgeSpecification);

        public static PersonalInfo Load(string nickname, string description, DateTime? birthDate, Sex sex, IMinimalAgeSpecification minimalAgeSpecification)
            => new PersonalInfo(nickname, description, birthDate, sex, minimalAgeSpecification);

        public PersonalInfo WithNickname(string nickname)
            => new PersonalInfo(nickname?.Trim(), Description, BirthDate, Sex, _minimalAgeSpecification);

        public PersonalInfo WithDescription(string description)
            => new PersonalInfo(Nickname, description?.Trim(), BirthDate, Sex, _minimalAgeSpecification);

        public PersonalInfo WithBirthDate(DateTime birthDate)
            => new PersonalInfo(Nickname, Description, birthDate, Sex, _minimalAgeSpecification);

        public PersonalInfo WithSex(Sex sex)
            => new PersonalInfo(Nickname, Description, BirthDate, sex, _minimalAgeSpecification);

        public PersonalInfo WithSex(int? sexValue)
            => WithSex(Sex.From(sexValue.GetValueOrDefault()));

        public bool Valid => !string.IsNullOrWhiteSpace(Nickname) &&
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
