using HitMeApp.Shared.DDD;

namespace HitMeApp.Users.Core.Exceptions
{
    internal class InvalidLongitudeException : DomainException
    {
        public override string Code => "invalid_longitude";
        public double Longitude { get; }

        public InvalidLongitudeException(double longitude) : base("")
        {
            Longitude = longitude;
        }
    }
}
