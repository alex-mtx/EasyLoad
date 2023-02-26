using Domain.Common;
using Domain.Common.Location;

namespace EasyLoad.Truckers.Domain.Common.Cmds
{
    public record CreateNewTruckerLocationCmd : Cmd
    {
        public CreateNewTruckerLocationCmd(Id id,Latitude latitude, Longitude longitude)
        {
            Id = id;
            Latitude = latitude;
            Longitude = longitude;
        }

        public Id Id { get; }
        public Latitude Latitude { get; }
        public Longitude Longitude { get; }
    }
}