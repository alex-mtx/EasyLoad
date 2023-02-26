using AutoFixture;
using Common.UnitTests;
using Domain.Common;
using Domain.Common.Location;
using Moq;
using TruckersApi.Queries.Infrastructure;
using TruckersApi.Queries.Repositories;

namespace TruckersApi.Queries.InfrastructureTests.Handlers
{
    [TestClass]
    public class TruckerByCoordinatesQueryHandlerShould : TestClass
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private Mock<ITruckerRepository> _repo;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        [TestInitialize]
        public void Setup()
        {
            _repo = _fixture.Freeze<Mock<ITruckerRepository>>();
        }


        [TestMethod]
        public async Task ReturnTruckersGivenAtLeastOneWasFound()
        {
            var sut = _fixture.Create<TruckerByCoordinatesQueryHandler>();
            var expectedTruckers = _fixture.CreateMany<Trucker>();
            var query = TruckersByLocationQueryBuilder.Create().Build();
            _repo.Setup(x => x.GetAll(query, _ct)).ReturnsAsync(expectedTruckers);

            var actualResult = await sut.Handle(query,_ct);

            Assert.AreSame(expectedTruckers, actualResult?.Value);
            Assert.AreEqual(Result.CreateOk(),actualResult?.State);
        }
    }

    public class TruckersByLocationQueryBuilder
    {
        private Latitude _latitude = new(10);
        private Longitude _longitude = new(10);
        private DistanceKm _distanceKm = new(100);

        public TruckersByLocationQueryBuilder WithLatitude(double newLatitude)
        {
            _latitude = new Latitude(newLatitude); 
            return this;
        }
        public TruckersByLocationQuery Build()
        {
            return new(_latitude, _longitude, _distanceKm);
        }
        public static TruckersByLocationQueryBuilder Create()
        {
            return new TruckersByLocationQueryBuilder();
        }
    }
    
}