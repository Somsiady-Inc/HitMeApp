using System.Collections.Generic;
using HitMeApp.Shared.DDD;
using HitMeApp.Users.Core.Exceptions;

namespace HitMeApp.Users.Core
{
    internal class Location : ValueObject
    {
        public double Latitude { get; }
        public double Longitude { get; }

        protected Location(double latitude, double longitude)
        {
            Validate(latitude, longitude);
            Latitude = latitude;
            Longitude = longitude;
        }

        private static void Validate(double latitude, double longitude)
        {
            if (latitude < -90 || latitude > 90)
            {
                throw new InvalidLatitudeException(latitude);
            }

            if (longitude < -180 || longitude > 180)
            {
                throw new InvalidLongitudeException(longitude);
            }
        }

        public static Location New(double latitude, double longitude)
            => new Location(latitude, longitude);

        public Location Shift(double shiftLatitude, double shiftLongitude)
            => new Location(Latitude + shiftLatitude, Longitude + shiftLongitude);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Latitude;
            yield return Longitude;
        }
    }
}
