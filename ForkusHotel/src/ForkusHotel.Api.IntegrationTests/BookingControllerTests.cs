using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ForkusHotel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Shouldly;
using Xunit;
// ReSharper disable InconsistentNaming
// ReSharper disable ClassNeverInstantiated.Local

namespace ForkusHotelApiIntegrationTests
{
    public class BookingControllerTests
    {
        private readonly HttpClient _apiClient;

        public BookingControllerTests()
        {
            // Arrange
            var server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());

            _apiClient = server.CreateClient();
        }

        private class HealtCheckDto
        {
            public bool isAlive { get; set; }
        }

        [Fact]
        public async Task HealthCheck()
        {
            var response = await _apiClient.GetAsync("/api/booking/health");
            var body = await FromBodyJson<HealtCheckDto>(response);

            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            body.isAlive.ShouldBe(true);
        }

        private class AllRoomTypesDto
        {
            public string[] roomTypes { get; set; }
        }

        [Fact]
        public async Task RetrieveAllRoomTypes()
        {
            var response = await _apiClient.GetAsync("/api/booking/roomtypes");

            var allRoomsDto = await FromBodyJson<AllRoomTypesDto>(response);

            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            allRoomsDto.roomTypes.ShouldContain("Single");
            allRoomsDto.roomTypes.ShouldContain("Double");
            allRoomsDto.roomTypes.ShouldContain("Twin");
            allRoomsDto.roomTypes.ShouldContain("DeluxeDouble");
            allRoomsDto.roomTypes.ShouldContain("JuniorSuite");
            allRoomsDto.roomTypes.ShouldContain("Suite");
            allRoomsDto.roomTypes.ShouldContain("ForkusSuite");
        }

        private async Task<T> FromBodyJson<T>(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            var body = JsonConvert.DeserializeObject<T>(content);

            return body;
        }
    }
}
