using Domain.Common.Location;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TruckersApi.Queries;

namespace TruckersApi.Models
{
    /// <summary>
    /// Use the <see href="https://earth-info.nga.mil">WGS-84</see> to represent a location, and the Distance to define the search radius.
    /// <para>
    /// Both longitude and latitude are angles and 
    /// represented in terms of degrees.
    /// </para>
    /// <para>
    /// Longitude values are measured from the 
    /// Prime Meridian and are between -180 degrees and 180.0 degrees.
    /// </para>
    /// <para>
    /// Latitude values are measured from the equator and are between -90.0 degrees and 90.0 degrees.
    /// </para>
    /// </summary>
    public record TruckerLocationModel
    {
        /// <summary>
        /// Latitude values are measured from the equator and are between -90.0 degrees and 90.0 degrees.
        /// </summary>
        [DefaultValue((double)48.133081)]
        [Range(-90,90)]
        public double Latitude { get; set; }

        /// <summary>
        /// Longitude values are measured from the Prime Meridian and are between -180 degrees and 180.0 degrees
        /// </summary>
        [DefaultValue((double)11.603796)]
        [Range(-180,180)]
        public double Longitude { get; set;}

        /// <summary>
        /// The search radius, in Kilometers.
        /// </summary>
        [DefaultValue(1000)]
        [Range(0,Domain.Common.Location.DistanceKm.Max)]
        public int Distance { get; set;}

        /// <summary>
        /// Creates a new instance of <see cref="TruckersByLocationQuery"/> based on the values of the <see cref="TruckerLocationModel"/>
        /// </summary>
        public TruckersByLocationQuery ToQuery()
        {
            var latitude = new Latitude(Latitude);
            var longitude = new Longitude(Longitude);
            var distance = new DistanceKm(Distance);
            return new TruckersByLocationQuery(latitude, longitude,distance);
        }
    }
 }
