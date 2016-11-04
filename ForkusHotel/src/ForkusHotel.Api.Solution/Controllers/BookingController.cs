using System;
using System.Net;
using ForkusHotel.Api.Solution.ReadModels;
using ForkusHotel.Api.Solution.WriteModels;
using Microsoft.AspNetCore.Mvc;
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

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
        [HttpGet("health")]
        public ActionResult HealthCheck()
        {
            return Ok(new HealthCheckResponseDto { isAlive = true });
        }

        // GET api/booking/roomtypes
        [HttpGet("roomtypes")]
        public ActionResult RetrieveAllRoomTypes()
        {
            return Ok(new RetrieveAllRoomTypesResponseDto
            {
                roomTypes = _bookingQueries.RetrieveAllRoomTypes()
            });
        }

        // POST api/booking/bookings
        [HttpPost("bookings")]
        public ActionResult NewBooking([FromBody]NewBookingRequestDto bookingRequestDto)
        {
            if (bookingRequestDto.numberOfNights < 1)
                return BadRequest(new ErrorResponseDto { error = "Specified time period is invalid"} );

            if (!_bookingCommands.IsValidRoomType(bookingRequestDto.roomType))
                return BadRequest(new ErrorResponseDto { error = "Unknown room-type specified" });

            if (_bookingCommands.IsCollision(bookingRequestDto.startDate, bookingRequestDto.numberOfNights,
                bookingRequestDto.roomType))
                return StatusCode( (int)HttpStatusCode.Conflict);

            var bookingId = _bookingCommands.BookARoom(bookingRequestDto.roomType, bookingRequestDto.startDate, bookingRequestDto.numberOfNights,
                bookingRequestDto.guestName);

            return Created($"api/booking/bookings/{bookingId}", new NewBookingSuccessResponseDto { bookingId = bookingId});
        }

        // Get api/booking/bookings
        [HttpGet("bookings")]
        public ActionResult RetrieveAllBookings()
        {
            var bookingsDto = _bookingQueries.RetrieveAllBookings();

            return Ok(bookingsDto);
        }

        // Get api/booking/bookings/{bookingId}
        [HttpGet("bookings/{bookingId}")]
        public ActionResult RetrieveBookingDetails(Guid bookingId)
        {
            var bookingDetailsDto = _bookingQueries.RetrieveBookingDetails(bookingId);

            if (bookingDetailsDto != null)
                return Ok(bookingDetailsDto);

            return NotFound();
        }

        // TODO: Make these contracts more explicit and versionable
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

        public class ErrorResponseDto { public string error { get; set; } }
    }
} 