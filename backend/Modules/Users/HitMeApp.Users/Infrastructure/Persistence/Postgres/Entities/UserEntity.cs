using System;
using System.Collections.Generic;

namespace HitMeApp.Users.Infrastructure.Persistence.Postgres.Entities
{
    internal class UserEntity
    {
        public Guid Id { get; init; }
        public double? Latitude { get; init; }
        public double? Longitude { get; init; }
        public string Nickname { get; init; }
        public string Description { get; init; }
        public DateTime? BirthDate { get; init; }
        public int? Sex { get; init; }
        public IEnumerable<TraitEntity> Traits { get; init; }
        public IEnumerable<TraitEntity> Preferences { get; init; }
    }
}
