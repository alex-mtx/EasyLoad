using Domain.Common;

namespace TruckersApi.Queries
{
    public record Trucker : IEntity
    {
        public string Id { get; set; } = new(Guid.NewGuid().ToString());
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public Location Location { get; set; } = new Location();
    }
}