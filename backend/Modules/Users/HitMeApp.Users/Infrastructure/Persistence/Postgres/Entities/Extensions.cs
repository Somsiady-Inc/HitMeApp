using System.Linq;
using HitMeApp.Users.Core;
using HitMeApp.Users.Core.Specification;

namespace HitMeApp.Users.Infrastructure.Persistence.Postgres.Entities
{
    internal static class Extensions
    {
        public static UserEntity AsDatabaseEntity(this User user)
            => new UserEntity
            {
                Id = user.Id.Value,
                Nickname = user.PersonalInfo?.Nickname,
                Description = user.PersonalInfo?.Description,
                BirthDate = user.PersonalInfo?.BirthDate,
                Sex = user.PersonalInfo is { } ? (byte)user.PersonalInfo.Sex.Value : null,
                Latitude = user.Location?.Latitude,
                Longitude = user.Location?.Longitude,
                Traits = user.Traits?.Select(trait => trait.AsDatabaseEntity()),
                Preferences = user.Preferences?.Select(preference => preference.AsDatabaseEntity()),
            };

        public static User AsDomainUser(this UserEntity userEntity)
            => User.Load(
                userEntity.Id,
                userEntity.Traits?.Select(trait => trait.AsDomainEntity()),
                userEntity.Preferences?.Select(preference => preference.AsDomainEntity()),
                PersonalInfo.Load(
                    userEntity.Nickname,
                    userEntity.Description,
                    userEntity.BirthDate,
                    userEntity.Sex is null ? Sex.NotKnown : Sex.From(userEntity.Sex.GetValueOrDefault()),
                    new MinimalAgeSpecification()
                )
            );

        public static TraitEntity AsDatabaseEntity(this Trait trait)
            => new TraitEntity
            {
                Id = trait.Id.Value,
                Value = trait.Value
            };

        public static Trait AsDomainEntity(this TraitEntity traitEntity)
            => Trait.Load(traitEntity.Id, traitEntity.Value);
    }
}
