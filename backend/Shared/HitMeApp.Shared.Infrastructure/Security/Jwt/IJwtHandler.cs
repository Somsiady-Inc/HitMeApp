using System;

namespace HitMeApp.Shared.Infrastructure.Security.Jwt
{
    public interface IJwtHandler
    {
        JsonWebToken Create(string email, Guid id);
    }
}
