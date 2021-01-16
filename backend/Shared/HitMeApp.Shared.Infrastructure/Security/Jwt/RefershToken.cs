using System;

namespace HitMeApp.Shared.Infrastructure.Security.Jwt
{
    public class RefershToken
    {
        public Guid UserId { get; set; }
        public string Token { get; set; }
        public bool Revoked { get; set; }
    }
}
