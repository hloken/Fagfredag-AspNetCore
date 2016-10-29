using Microsoft.AspNetCore.Mvc;

// ReSharper disable InconsistentNaming
// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace ForkusHotel.Api.Controllers
{
    [Route("api/booking")]
    public class BookingController : Controller
    {
        // GET api/booking
        [HttpGet("health")]
        public ActionResult HealthCheck()
        {
            return Ok(new HealthCheckDto { isAlive = true});
        }

        private class HealthCheckDto
        {
            public bool isAlive { get; set; }
        }
    }
}