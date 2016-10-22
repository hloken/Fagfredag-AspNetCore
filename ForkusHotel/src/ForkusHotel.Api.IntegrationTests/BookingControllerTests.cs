using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ForkusHotel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Shouldly;
using Xunit;

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

        [Fact]
        public async Task HealthCheck()
        {
            var response = await _apiClient.GetAsync("/api/booking/health");

            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }
    }
}
