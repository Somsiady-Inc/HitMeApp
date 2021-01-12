using System;
using HitMeApp.Shared.DDD;

namespace HitMeApp.Users.Core.Specification
{
    internal class MinimalAgeSpecification : ISpecification<PersonalInfo>
    {
        // TODO: Can be based on locale later on
        public bool IsSatisfiedBy(PersonalInfo entity)
            => entity.BirthDate != default && DateTime.Now >= entity.BirthDate.AddYears(18);
    }
}
