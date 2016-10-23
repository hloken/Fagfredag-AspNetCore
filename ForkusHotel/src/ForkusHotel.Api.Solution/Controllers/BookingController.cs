using System;
using System.Net;
using ForkusHotel.Api.Solution.Persistence;
using Microsoft.AspNetCore.Mvc;

// ReSharper disable InconsistentNaming
// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace ForkusHotel.Api.Solution.Controllers
{
    [Route("api/booking")]
    public class BookingController : Controller
    {
        private readonly IBookingCommands _bookingCommands;
        private readonly IBookingQueries _bookingQueries;

        public BookingController(IBookingCommands bookingCommands, IBookingQueries bookingQueries)
        {
            _bookingCommands = bookingCommands;
            _bookingQueries = bookingQueries;
        }

        // GET api/booking/health
        [HttpGet]
        [Route("health")]
        public ActionResult HealthCheck()
        {
            return Json(new HealthCheckResponseDto { isAlive = true });
        }

        // GET api/booking/roomtypes
        [HttpGet]
        [Route("roomtypes")]
        public ActionResult RetrieveAllRoomTypes()
        {
            return Json(new RetrieveAllRoomTypesResponseDto
            {
                roomTypes = new[] { "Single", "Double", "Twin", "DeluxeDouble", "JuniorSuite", "Suite", "ForkusSuite" }
            });
        }

        // POST api/booking/bookings
        [HttpPost]
        [Route("bookings")]
        public ActionResult NewBooking([FromBody]NewBookingRequestDto bookingRequestDto)
        {
            if (bookingRequestDto.numberOfNights < 1)
                return BadRequest(new ErrorResponseDto { error = "Specified time period is invalid"} );

            var bookingId = _bookingCommands.NewBooking(bookingRequestDto.roomType, bookingRequestDto.startDate, bookingRequestDto.numberOfNights,
                bookingRequestDto.guestName);

            return Created($"api/booking/bookings/{bookingId}", new NewBookingSuccessResponseDto { bookingId = bookingId});
        }

        public class HealthCheckResponseDto { public bool isAlive { get; set; } }

        public class RetrieveAllRoomTypesResponseDto { public string[] roomTypes { get; set; } }

        public class NewBookingRequestDto
        {
            public string roomType { get; set; }
            public DateTime startDate { get; set; }
            public int numberOfNights { get; set; }
            public string guestName { get; set; }
        }

        public class NewBookingSuccessResponseDto { public Guid bookingId { get; set; } }

        public class ErrorResponseDto {
            public string error { get; set; } }
    }
}