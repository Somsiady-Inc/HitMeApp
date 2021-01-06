using HitMeApp.Indentity.Core;

namespace HitMeApp.Indentity.Infrastructure.Entities
{
    internal static class Extensions
    {
        public static UserEntity AsDatabaseEntity(this User user)
            => new UserEntity
            {
                Id = user.Id.Value,
                Email = user.Email,
                Password = user.Password,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };

        public static User AsDomainEntity(this UserEntity user)
            => User.Load(new UserId(user.Id), user.Email, user.Password, user.CreatedAt, user.UpdatedAt);
    }
}
