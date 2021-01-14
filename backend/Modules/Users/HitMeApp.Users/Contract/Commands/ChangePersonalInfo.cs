using System;
using HitMeApp.Users.Contract.Dtos;

namespace HitMeApp.Users.Contract.Commands
{
    public class ChangePersonalInfo : IUserCommand<UserDto>
    {
        public Guid UserId { get; set; }
        public string Nickname { get; set; }
        public string Description { get; set; }
        public DateTime BirthDate { get; set; }
        public int? Sex { get; set; }
    }
}
