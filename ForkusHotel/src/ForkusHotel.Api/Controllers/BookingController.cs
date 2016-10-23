using Microsoft.AspNetCore.Mvc;
// ReSharper disable InconsistentNaming

namespace ForkusHotel.Controllers
{
    [Route("api/booking")]
    public class BookingController : Controller
    {
        // GET api/booking
        [HttpGet]
        [Route("health")]
        public JsonResult HealthCheck()
        {
            return Json(new HealthCheckDto { isAlive = true});
        }

        internal class HealthCheckDto
        {
            public bool isAlive
            {
                get; set;
            }
        }
    }
}