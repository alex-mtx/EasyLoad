using Domain.Common;
using EasyLoad.Truckers.Domain.Common;

namespace EasyLoad.Truckers.Domain
{
    public record Trucker : IEntity
    {
        public Trucker(Id id, FirstName firstName, LastName lastName, Location? location)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Location = location ?? Location.NewUnknownLocation();
        }
        /// <summary>
        /// Used only for JSON deserialization
        /// </summary>
        private Trucker()
        {

        }

        public string Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public Location Location { get; private set; }

        public void UpdateLocation(Location location)
        {
            Location = location;
        }
    }
}