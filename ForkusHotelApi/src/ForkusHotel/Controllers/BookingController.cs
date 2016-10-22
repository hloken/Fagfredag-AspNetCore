using Microsoft.AspNetCore.Mvc;

namespace ForkusHotel.Controllers
{
    [Route("api/booking")]
    public class BookingController : Controller
    {
        // GET api/booking
        [HttpGet]
        [Route("health")]
        public bool HealthCheck()
        {
            return true;
        }
    }
}