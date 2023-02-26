using EasyLoad.Truckers.Domain;
using EasyLoad.Truckers.Infrastructure;
using EasyLoad.Truckers.Infrastructure.CosmosDbServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EasyLoad.Truckers.Ioc
{
    public static class ConfigureTruckerServices
    {
        public static void AddTruckersDomainServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IGeoLocationService, GeoLocationService>();
            services.AddTransient<ITruckerRepository, TruckerRepository>();
            services.AddMediatR(r => r.RegisterServicesFromAssembly(typeof(CreateNewTruckerLocationHandler).Assembly));

        }

    }
}