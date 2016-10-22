using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ForkusHotel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;
using Shouldly;

namespace ForkusHotelApiIntegrationTests
{
    [TestFixture]
    public class BookingControllerTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public BookingControllerTests()
        {
            // Arrange
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [Test]
        public async Task HealthCheck()
        {
            var response = await _client.GetAsync("/api/booking/health");

            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }
    }
}
