using Domain.Common.Location;
using MediatR;
using System.Globalization;

namespace TruckersApi.Queries
{
    public record TruckersByLocationQuery : IRequest<TruckersByCoordinatesResponse>
    {
        public double Latitude { get; }
        public double Longitude { get; }
        public int Distance { get; }

        public TruckersByLocationQuery(Latitude latitude, Longitude longitude, DistanceKm distance)
        {
            Latitude = latitude.Value;
            Longitude = longitude.Value;
            Distance = distance.GetMeters;
        }

        public override string ToString()
        {
            return $"{Convert.ToString(Longitude, CultureInfo.InvariantCulture)},{Convert.ToString(Latitude, CultureInfo.InvariantCulture)}";
        }
    }
}