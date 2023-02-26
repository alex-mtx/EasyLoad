using TruckersApi.Queries.CosmosDbServices;
using TruckersApi.Queries.Infrastructure;
using TruckersApi.Queries.Repositories;

namespace TruckersApi
{
    public static class ApiConfigureServices
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<TruckerByCoordinatesQueryHandler>());
            services.AddTransient<ITruckerRepository, TruckerCosmosRepository>();
            return services;
        }

    }
}
