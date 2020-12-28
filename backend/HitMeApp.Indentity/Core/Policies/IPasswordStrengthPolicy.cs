using HitMeApp.Shared.DDD;

namespace HitMeApp.Indentity.Core.Policies
{
    internal interface IPasswordStrengthPolicy : IPolicy
    {
        public void Test(string password);
    }
}
