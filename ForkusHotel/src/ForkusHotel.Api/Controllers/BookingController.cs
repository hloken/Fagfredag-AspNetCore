using Microsoft.AspNetCore.Mvc;

// ReSharper disable InconsistentNaming
// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace ForkusHotel.Api.Controllers
{
    //[Route("api/booking")]    
    [Route("api/bookingBase")]
    public class BookingController : Controller
    {
        // GET api/booking
        [HttpGet]
        [Route("health")]
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