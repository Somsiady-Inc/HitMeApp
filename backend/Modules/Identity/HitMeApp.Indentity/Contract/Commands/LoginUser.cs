using System;

namespace HitMeApp.Indentity.Contract.Commands
{
    public class LoginUser : IIdentityCommand<Guid>
    {
        public string Email { get; init; }
        public string Password { get; init; }
    }
}
