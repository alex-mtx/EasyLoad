using Common.CosmosDbServices;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;
using TruckersApi.Queries.Repositories;

namespace TruckersApi.Queries.CosmosDbServices
{
    public class TruckerCosmosRepository : CosmosDbService<Trucker>, ITruckerRepository
    {
        public TruckerCosmosRepository(CosmosClient dbClient, IOptions<CosmosDbOptions> options) : base(dbClient, options)
        {
        }

        protected override string ContainerName => "Location";
        protected override string DatabaseName => "Truckers";

        public async Task<IEnumerable<Trucker>> GetAll(TruckersByLocationQuery query, CancellationToken ct = default)
        {
            return await GetItemsAsync(
                @$"SELECT * FROM {ContainerName} c 
                    WHERE ST_DISTANCE(c.location,{{""type"":""Point"",""coordinates"":[{query}]}}) < {query.Distance}", ct);
        }
    }
}