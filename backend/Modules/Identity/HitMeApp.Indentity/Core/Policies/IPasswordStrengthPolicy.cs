using HitMeApp.Shared.DDD;

namespace HitMeApp.Indentity.Core.Policies
{
    internal interface IPasswordStrengthPolicy : IPolicy
    {
        public void Validate(string password);
    }
}
