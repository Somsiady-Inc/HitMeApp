using HitMeApp.Shared.DDD;

namespace HitMeApp.Indentity.Core.Policies
{
    public interface IEmailValidityPolicy : IPolicy
    {
        public void Validate(string email);
    }
}
