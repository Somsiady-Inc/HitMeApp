using System;

namespace HitMeApp.Users.Contract.Commands
{
    public class ChangeLocation : IUserCommand
    {
        public Guid UserId { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
