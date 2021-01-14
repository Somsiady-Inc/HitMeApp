using HitMeApp.Users.Core;

namespace HitMeApp.Users.Contract.Dtos
{
    internal static class Extensions
    {
        public static UserDto AsDto(this User user)
            => new UserDto
            {
                Id = user.Id.Value,
                Nickname = user.PersonalInfo?.Nickname,
                Description = user.PersonalInfo?.Description,
                BirthDate = user.PersonalInfo?.BirthDate,
                Sex = user.PersonalInfo?.Sex is null ? (byte)Sex.NotKnown.Value : (byte)user.PersonalInfo.Sex.Value,
                Longitude = user.Location?.Longitude,
                Latitude = user.Location?.Latitude
            };
    }
}
