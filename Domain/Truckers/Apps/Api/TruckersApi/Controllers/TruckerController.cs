using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Swashbuckle.AspNetCore.Annotations;
using TruckersApi.Models;
using TruckersApi.Queries;

namespace TruckersApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("truckers"), Produces("application/json")]
    public class TruckerController : ControllerBase
    {
        private readonly ILogger<TruckerController> _logger;
        private readonly IMediator _mediator;
        public const string LocationReaderRole = "Location.Readers";
        public const string LocationWriterRole = "Location.Writers";
        public const string LocationRoute = "location";

        public TruckerController(ILogger<TruckerController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        /// <remarks>
        /// Searches for all Truckers that are in the given location, using latitude and longitude, according to the WGS-84 standard. It also expands the search radius according to the distance value.
        /// <para>
        /// Sample request:
        ///
        ///     Get /truckers/location?longitude=11.603796&amp;latitude=48.133081&amp;distance=1000
        ///</para>
        /// </remarks>
        [HttpGet]
        [Route(LocationRoute)]
        [SwaggerOperation(
            Summary = "Searches for Truckers in a given location.",
            OperationId = "SearchTruckersInLocation",
            Tags = new[] { "Location" }
         )]
        [SwaggerResponse(StatusCodes.Status200OK, "The query was successful and at least one Trucker was found.", typeof(List<Trucker>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The request is invalid and won't be processed.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The query was successful, but NO Trucker was found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "The operation failed given an internal server error.")]
        [RequiredScopeOrAppPermission(
            AcceptedScope = new string[] { LocationReaderRole, LocationWriterRole },
            AcceptedAppPermission = new string[] { LocationReaderRole, LocationWriterRole })]
        public async Task<IActionResult> GetTruckersByLocation([FromQuery] TruckerLocationModel model,CancellationToken ct)
        {
            var result = await _mediator.Send(model.ToQuery(), ct);
            if(result.Success)
                return Ok(result.Value);
            return StatusCode(result.StatusCode,null);
        }

        [HttpPut]
        [Route("{id}/location")]
        [SwaggerOperation(
          Summary = "Persists the current trucker's location.",
          OperationId = "CreateTruckersInLocation",
          Tags = new[] { "Location" }
       )]
        [SwaggerResponse(StatusCodes.Status204NoContent, "The operation was successful.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The request is invalid and won't be processed.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "The operation failed given an internal server error.")]
        [RequiredScopeOrAppPermission(
            AcceptedScope = new string[] { LocationWriterRole },
            AcceptedAppPermission = new string[] { LocationWriterRole })]
        public async Task<IActionResult> Put([FromRoute] string id,[FromBody] NewTruckerLocationModel model, CancellationToken ct)
        {
            var result = await _mediator.Send(model.ToCmd(id), ct);
            if (result.Success)
                return StatusCode(204);
            return StatusCode(result.StatusCode, null);
        }

    }
}