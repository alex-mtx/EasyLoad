using EasyLoad.Truckers.Domain.Common;

namespace EasyLoad.Truckers.Domain
{
    public record UnknownLocation : Location
    {
        public UnknownLocation() : base(latitude:new(0), longitude: new(0), new City("Unknown"), new Region("Unknown"), new Country("Unknown"))
        {
        }
    }
}