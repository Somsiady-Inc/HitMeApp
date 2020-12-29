using HitMeApp.Indentity.Core;

namespace HitMeApp.Indentity.Contract.Dtos
{
    internal static class Extensions
    {
        public static UserDto AsDto(this User user)
            => new UserDto
            {
                Id = user.Id,
                Email = user.Email
            };
    }
}
