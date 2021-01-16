using System;
using HitMeApp.Shared.Infrastructure.Security.Jwt;

namespace HitMeApp.Indentity.Contract.Commands
{
    public class LoginUser : IIdentityCommand<JsonWebToken>
    {
        public string Email { get; init; }
        public string Password { get; init; }
    }
}
