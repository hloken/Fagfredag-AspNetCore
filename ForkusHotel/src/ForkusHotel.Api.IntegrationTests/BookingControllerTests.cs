using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ForkusHotel.Api.Solution;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Shouldly;
using Xunit;
using static ForkusHotelApiIntegrationTests.TestUtils;

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
            var body = await response.GetBodyAsJson<HealtCheckDto>();

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

            var allRoomsDto = await response.GetBodyAsJson<AllRoomTypesDto>();

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
            var content = new
            {
                roomType = "ForkusSuite",
                startDate = "2016-10-21T13:28:06.419Z",
                numberOfNights = 3,
                guestName = "Kjell Lj0stad"
            }.ToJsonStringContent();

            var response = await _apiClient.PostAsync($"{bookingServicePath}/bookings", content);

            response.StatusCode.ShouldBe(HttpStatusCode.Created);

            var bookingId = (await response.GetBodyAsJson<RoomBookingResponseDto>()).bookingId;
            response.Headers.Location.OriginalString.ShouldBe($"api/booking/bookings/{bookingId}");
        }

        [Fact]
        public async Task BookARoom_WithInvalidTimePeriod()
        {
            var content = new
            {
                roomType = "ForkusSuite",
                startDate = "2016-10-21T13:28:06.419Z",
                numberOfNights = -1,
                guestName = "Kjell Lj0stad"
            }.ToJsonStringContent();

            var response = await _apiClient.PostAsync($"{bookingServicePath}/bookings", content);

            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

            var errorMessage = (await response.GetBodyAsJson<ErrorResponseDto>()).error;
            errorMessage.ShouldNotBe(string.Empty);
        }

        [Fact]
        public async Task RetrieveListOfAllBookings_WithOneBooking()
        {
            // Arrange
            var bookingId = await _apiClient.SetupABooking();

            // Act
            var response = await _apiClient.GetAsync($"{bookingServicePath}/bookings");

            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var bookingsDto = (await response.GetBodyAsJson<BookingListDto>());
            bookingsDto.bookings.Length.ShouldBe(1);

            var firstBooking = bookingsDto.bookings[0];
            firstBooking.bookingId.ShouldBe(bookingId);
        }
    }

    
}
