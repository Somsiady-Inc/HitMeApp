using System;

namespace HitMeApp.Users.Infrastructure.Persistence.Postgres.Entities
{
    internal class TraitEntity
    {
        public Guid Id { get; init; }
        public string Value { get; init; }
    }
}
