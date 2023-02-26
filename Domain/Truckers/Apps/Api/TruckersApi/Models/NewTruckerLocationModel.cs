using Domain.Common;
using Domain.Common.Location;
using EasyLoad.Truckers.Domain.Common.Cmds;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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
    public record NewTruckerLocationModel
    {
        /// <summary>
        /// Latitude values are measured from the equator and are between -90.0 degrees and 90.0 degrees.
        /// </summary>
        [Range(-90, 90)]
        [DefaultValue((double)52.513448)]

        public double Latitude { get; set; }

        /// <summary>
        /// Longitude values are measured from the Prime Meridian and are between -180 degrees and 180.0 degrees
        /// </summary>
        [DefaultValue((double)13.40344)]
        [Range(-180, 180)]
        public double Longitude { get; set; }

        public CreateNewTruckerLocationCmd ToCmd(string id) => new(new Id(id), new Latitude(Latitude), new Longitude(Longitude));
    }
}
