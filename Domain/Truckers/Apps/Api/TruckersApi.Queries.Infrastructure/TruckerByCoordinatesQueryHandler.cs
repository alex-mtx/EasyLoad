using Domain.Common;
using Domain.Common.Handlers;
using MediatR;
using Microsoft.Extensions.Logging;
using TruckersApi.Queries.Repositories;

namespace TruckersApi.Queries.Infrastructure
{
    public class TruckerByCoordinatesQueryHandler : Handler<TruckersByLocationQuery, TruckersByCoordinatesResponse>
    {
        public ITruckerRepository Repository { get; }

        public TruckerByCoordinatesQueryHandler(ITruckerRepository repository,ILogger<TruckerByCoordinatesQueryHandler> logger): base(logger)
        {
            Repository = repository;
        }

        protected override async Task<TruckersByCoordinatesResponse> HandleInternally(TruckersByLocationQuery request, CancellationToken ct)
        {
            var truckers = await Repository.GetAll(request, ct);
            var result = Result.CreateNotFound();
            if (truckers.Any())
                result = Result.CreateOk();

            return new TruckersByCoordinatesResponse(truckers,result);
        }

        protected override Task<TruckersByCoordinatesResponse> DefaultErrorResponse()
        {
            return Task.FromResult(new TruckersByCoordinatesResponse(null, Result.CreateInternalError()));
        }
    }
}
