using System;

namespace HitMeApp.Indentity.Infrastructure.Entities
{
    internal class UserEntity
    {
        public Guid Id { get; init; }
        public string Email { get; init; }
        public string Password { get; init; }
        public DateTime CreatedAt { get; init; }
        public DateTime UpdatedAt { get; init; }
    }
}
