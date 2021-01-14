using System;

namespace HitMeApp.Users.Contract.Dtos
{
    public class UserDto
    {
        public Guid Id { get; init; }
        public double? Latitude { get; init; }
        public double? Longitude { get; init; }
        public string Nickname { get; init; }
        public string Description { get; init; }
        public DateTime? BirthDate { get; init; }
        public byte? Sex { get; init; }
    }
}
