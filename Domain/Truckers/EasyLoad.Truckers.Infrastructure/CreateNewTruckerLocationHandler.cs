using Domain.Common;
using Domain.Common.Handlers;
using Domain.Common.Location;
using EasyLoad.Truckers.Domain;
using EasyLoad.Truckers.Domain.Common;
using EasyLoad.Truckers.Domain.Common.Cmds;
using Microsoft.Extensions.Logging;

namespace EasyLoad.Truckers.Infrastructure
{
    public class CreateNewTruckerLocationHandler : Handler<CreateNewTruckerLocationCmd, Result>
    {
        private readonly ITruckerRepository _repository;
        private readonly IGeoLocationService _geoService;

        public CreateNewTruckerLocationHandler(ITruckerRepository repository, IGeoLocationService geoService, ILogger<CreateNewTruckerLocationHandler> logger) : base(logger)
        {
            _repository = repository;
            _geoService = geoService;
        }

        protected override Task<Result> DefaultErrorResponse()
        {
            return Task.FromResult(Result.CreateInternalError());
        }

        protected override async Task<Result> HandleInternally(CreateNewTruckerLocationCmd request, CancellationToken cancellationToken)
        {
            var trucker = await _repository.GetAsync(request.Id, cancellationToken);
            if (trucker == null)
            {
                trucker = new Trucker(request.Id, new FirstName("John"), new LastName("Doe"), Location.NewUnknownLocation());
                await _repository.UpdateAsync(request.Id, trucker, cancellationToken);

            }
            var location = await _geoService.GetAsync(request.Latitude, request.Longitude);
            if (location == null)
            {
                location = Location.NewUnknownLocation();
            }
            trucker.UpdateLocation(location);

            await _repository.UpdateAsync(trucker.Id, trucker, cancellationToken);

            return Result.CreateNoContent();
        }
    }

    public interface IGeoLocationService
    {
        Task<Location> GetAsync(Latitude latitude, Longitude longitude);
    }
    public class GeoLocationService : IGeoLocationService
    {
        public Task<Location> GetAsync(Latitude latitude, Longitude longitude)
        {
            return Task.FromResult(new Location(latitude, longitude, new("Berlin"), new("Berlin"), new("Germany")));
        }
    }


}

