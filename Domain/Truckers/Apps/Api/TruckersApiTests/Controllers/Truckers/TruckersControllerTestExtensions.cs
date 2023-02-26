using AutoFixture;
using TruckersApi.Controllers;

namespace TruckersApiTests.Controllers.Truckers
{
    public static class TruckersControllerTestExtensions
    {
       public static TruckerController CreateTruckersController(this IFixture fixture)
        {
            return fixture.Build<TruckerController>().OmitAutoProperties().Create();
        }
    }
}
