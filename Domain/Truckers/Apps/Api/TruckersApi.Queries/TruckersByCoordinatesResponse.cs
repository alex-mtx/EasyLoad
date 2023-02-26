using Common.Queries;
using Domain.Common;

namespace TruckersApi.Queries
{
    public record TruckersByCoordinatesResponse : QueryResponse<IEnumerable<Trucker>>
    {
        public TruckersByCoordinatesResponse(IEnumerable<Trucker>? value, Result state) : base(state)
        {
            Value = value ?? new List<Trucker>();
        }

        public override IEnumerable<Trucker> Value { get; } = new List<Trucker>();
    }
}