using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ForkusHotel.Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Shouldly;
using Xunit;
// ReSharper disable InconsistentNaming
// ReSharper disable ClassNeverInstantiated.Local
// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace ForkusHotelApiIntegrationTests
{
    public class BookingControllerTests
    {
        private const string bookingServicePath = "api/booking";
        private readonly HttpClient _apiClient;

        public BookingControllerTests()
        {
            // Arrange
            var server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());

            _apiClient = server.CreateClient();
        }

        [Fact]
        public async Task HealthCheck()
        {
            var response = await _apiClient.GetAsync($"{bookingServicePath}/health");
            var body = await FromBodyJson<HealtCheckDto>(response);

            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            body.isAlive.ShouldBe(true);
        }

        private class HealtCheckDto
        {
            public bool isAlive { get; set; }
        }

        [Fact]
        public async Task RetrieveAllRoomTypes()
        {
            var response = await _apiClient.GetAsync($"{bookingServicePath}/roomtypes");

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

        [Fact]
        public async Task BookARoom_WithValidRequestAndRoomIsAvailable()
        {
            var content = ToJsonStringContentFrom(new
            {
                roomType = "ForkusSuite",
                startDate = "2016-10-21T13:28:06.419Z",
                numberOfNights = 3,
                guestName = "Kjell Lj0stad"
            });
            var response = await _apiClient.PostAsync($"{bookingServicePath}/bookings", content);

            response.StatusCode.ShouldBe(HttpStatusCode.Created);

            var bookingId = (await FromBodyJson<RoomBookingResponseDto>(response)).bookingId;
            response.Headers.Location.OriginalString.ShouldBe($"api/booking/bookings/{bookingId}");
        }

        [Fact]
        public async Task BookARoom_WithInvalidTimePeriod()
        {
            var content = ToJsonStringContentFrom(new
            {
                roomType = "ForkusSuite",
                startDate = "2016-10-21T13:28:06.419Z",
                numberOfNights = 03,
                guestName = "Kjell Lj0stad"
            });
            var response = await _apiClient.PostAsync($"{bookingServicePath}/bookings", content);

            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

            var bookingId = (await FromBodyJson<RoomBookingResponseDto>(response)).bookingId;
        }

        private class RoomBookingResponseDto { public Guid bookingId { get; set; } }
        private class AllRoomTypesDto { public string[] roomTypes { get; set; } }

        private StringContent ToJsonStringContentFrom<T>(T dtoObject)
        {
            var json = JsonConvert.SerializeObject(dtoObject);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            return content;
        }

        private async Task<T> FromBodyJson<T>(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            var body = JsonConvert.DeserializeObject<T>(content);

            return body;
        }
    }
}
