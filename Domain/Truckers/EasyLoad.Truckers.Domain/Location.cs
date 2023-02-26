using Domain.Common.Location;
using EasyLoad.Truckers.Domain.Common;

namespace EasyLoad.Truckers.Domain
{
    public record Location
    {
        public Location(Latitude latitude, Longitude longitude, City city, Region region, Country country)
        {
            Latitude = latitude;
            Longitude = longitude;
            Country = country;
            City = city;
            Region = region;
        }

        /// <summary>
        /// Used only for JSON deserialization
        /// </summary>
        private Location()
        {

        }

        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        public string City { get; private set; }
        public string Region { get; private set; }
        public string Country { get; private set; }
        public string Type => "Point";
        public double[] Coordinates => new double[2] { Longitude, Latitude };

        public static Location NewUnknownLocation() => new UnknownLocation();
    }
}