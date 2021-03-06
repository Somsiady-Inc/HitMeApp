﻿using HitMeApp.Indentity.Application.Exceptions;
using HitMeApp.Indentity.Core;
using HitMeApp.Indentity.Core.Policies;
using HitMeApp.Indentity.Core.Services;
using Microsoft.AspNetCore.Identity;

namespace HitMeApp.Indentity.Application.Security
{
    internal sealed class PasswordHasherBasedUserPasswordService : IUserPasswordService
    {
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IPasswordStrengthPolicy _passwordStrengthPolicy;

        public PasswordHasherBasedUserPasswordService(IPasswordHasher<User> passwordHasher, IPasswordStrengthPolicy passwordStrengthPolicy)
        {
            _passwordHasher = passwordHasher;
            _passwordStrengthPolicy = passwordStrengthPolicy;
        }

        public string GeneratePasswordForUser(string password)
        {
            _passwordStrengthPolicy.Validate(password);
            return _passwordHasher.HashPassword(null, password);
        }

        public bool Verify(string currentHash, string newPassword)
        {
            var verificationResult = _passwordHasher.VerifyHashedPassword(null, currentHash, newPassword);
            return verificationResult != PasswordVerificationResult.Failed;
        }
    }
}
