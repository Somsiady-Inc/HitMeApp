using HitMeApp.Shared.DDD;

namespace HitMeApp.Users.Core.Exceptions
{
    internal class CannotUpdateIncompleteUserException : DomainException
    {
        public override string Code => "cannot_update_incomplete_user";

        public string ParameterName { get; }

        public CannotUpdateIncompleteUserException(string parameterName)
            : base($"Cannot update the parameter for an incomplete user: {parameterName}")
        {
            ParameterName = parameterName;
        }
    }
}
