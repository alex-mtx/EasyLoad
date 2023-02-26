using AutoFixture;
using Common.UnitTests;
using Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Moq;
using TruckersApi.Controllers;
using TruckersApi.Models;
using TruckersApi.Queries;

namespace TruckersApiTests.Controllers.Truckers
{
    [TestClass]
    [TestCategory("Truckers.Location")]
    [TestCategory("UnitTest")]
    public class WhenGetLocationsShould : TestClass
    {
        [TestInitialize]
        public void Setup()
        {
            _mediator = _fixture.Freeze<Mock<IMediator>>();
        }


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private Mock<IMediator> _mediator;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        [TestMethod]
        public void DefineRoute()
        {
            var actualRoute = MvcAttributeBuilder.ExtractAttributeFrom<RouteAttribute, TruckerController>(nameof(TruckerController.GetTruckersByLocation));

            Assert.IsNotNull(actualRoute, "Route attribute is missing");
            Assert.AreEqual(TruckerController.LocationRoute, actualRoute.Template);

        }

        [TestMethod]
        public void DefineGetAttribute()
        {
            var actualHttpAttribute = MvcAttributeBuilder.ExtractAttributeFrom<HttpGetAttribute, TruckerController>(nameof(TruckerController.GetTruckersByLocation));

            Assert.IsNotNull(actualHttpAttribute, "HttpGet attribute is missing");

        }

        [TestMethod]
        public void DefineRequiredScopes()
        {
            var actualRequiredScopes = MvcAttributeBuilder.ExtractAttributeFrom<RequiredScopeOrAppPermissionAttribute, TruckerController>(nameof(TruckerController.GetTruckersByLocation));

            Assert.IsNotNull(actualRequiredScopes, "RequiredScopeOrAppPermission attribute is missing");
            CollectionAssert.AreEqual(new string[] { TruckerController.LocationReaderRole, TruckerController.LocationWriterRole }, actualRequiredScopes.AcceptedScope);
            CollectionAssert.AreEqual(new string[] { TruckerController.LocationReaderRole, TruckerController.LocationWriterRole }, actualRequiredScopes.AcceptedAppPermission);
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(3)]
        public async Task ReturnOkGivenTruckerWasFound(int numberOfTruckersFound)
        {
            var validModel = new TruckerLocationModel
            {
                Distance = 10,
                Latitude = 10,
                Longitude = 10,
            };
            var expectedTruckers = _fixture.CreateMany<Trucker>(numberOfTruckersFound);
            var expectedResult = new TruckersByCoordinatesResponse(expectedTruckers, Result.CreateOk());
            var sut = _fixture.CreateTruckersController();
            _mediator.Setup(x => x.Send(It.IsAny<TruckersByLocationQuery>(), _ct)).ReturnsAsync(expectedResult);

            //Act
            var actualResult = await sut.GetTruckersByLocation(validModel, _ct) as OkObjectResult;

            Assert.IsNotNull(actualResult, "OkObjectResult is expected");
            CollectionAssert.AreEquivalent(expectedResult.Value.ToList(), (actualResult?.Value as IEnumerable<Trucker>)?.ToList());

        }

        [TestMethod]
        public async Task Return404GivenNoTruckerWasFound()
        {
            var validModel = new TruckerLocationModel
            {
                Distance = 10,
                Latitude = 10,
                Longitude = 10,
            };
            var expectedResult = new TruckersByCoordinatesResponse(null, Result.CreateNotFound());
            var sut = _fixture.CreateTruckersController();
            _mediator.Setup(x => x.Send(It.IsAny<TruckersByLocationQuery>(), _ct)).ReturnsAsync(expectedResult);

            //Act
            var actualResult = await sut.GetTruckersByLocation(validModel, _ct) as ObjectResult;

            Assert.IsNotNull(actualResult,"ObjectResult is expected");
            Assert.IsNull(actualResult.Value);
            Assert.AreEqual(StatusCodes.Status404NotFound, actualResult.StatusCode);

        }

        //there other scenarios to test
    }
}
