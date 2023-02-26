using Common.CosmosDbServices;
using EasyLoad.Truckers.Domain;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;

namespace EasyLoad.Truckers.Infrastructure.CosmosDbServices
{
    public class TruckerRepository : CosmosDbService<Trucker>, ITruckerRepository
    {
        public TruckerRepository(CosmosClient dbClient, IOptions<CosmosDbOptions> options) : base(dbClient, options)
        {
        }

        protected override string ContainerName { get; } = "Location";
        protected override string DatabaseName { get; } = "Truckers";
    }
}
