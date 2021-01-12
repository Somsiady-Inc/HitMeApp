using System;
using HitMeApp.Indentity.Application.Models;

namespace HitMeApp.Indentity.Application.Services
{
    public interface IJwtHandler
    {
        JsonWebToken Create(string email, Guid id);
    }
}
