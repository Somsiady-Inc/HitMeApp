using HitMeApp.Shared.DDD;

namespace HitMeApp.Users.Core.Exceptions
{
    internal class InvalidLatitudeException : DomainException
    {
        public override string Code => "invalid_latitude";
        public double Latitude { get; }

        public InvalidLatitudeException(double latitude) : base("")
        {
            Latitude = latitude;
        }
    }
}
