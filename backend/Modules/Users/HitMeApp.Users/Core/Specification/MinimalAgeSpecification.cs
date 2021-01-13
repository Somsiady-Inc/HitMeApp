using System;
namespace HitMeApp.Users.Core.Specification
{
    internal class MinimalAgeSpecification : IMinimalAgeSpecification
    {
        // TODO: Can be based on locale later on
        public bool IsSatisfiedBy(PersonalInfo entity)
            => entity.BirthDate.HasValue && entity.BirthDate != default && DateTime.Now >= entity.BirthDate.Value.AddYears(18);
    }
}
