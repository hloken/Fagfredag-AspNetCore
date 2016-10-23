using System;
using System.Collections.Generic;

namespace ForkusHotel.Api.Solution.Persistence
{
    internal class BookingStore : IBookingCommands, IBookingQueries
    {
        private List<Booking> _bookings = new List<Booking>();

        public Guid NewBooking(string roomType, DateTime startDate, int numberOfNights, string guestName)
        {
            var bookingId = Guid.NewGuid();

            _bookings.Add(Booking.Create(bookingId, roomType, startDate, numberOfNights, guestName));

            return bookingId;
        }

        private class Booking
        {
            internal static Booking Create(Guid bookingId, string roomType, DateTime startDate, int numberOfNights, string guestName)
            {
                return new Booking
                {
                    BookingId = bookingId,
                    RoomType = roomType,
                    StartDate = startDate,
                    NumberOfNights = numberOfNights,
                    GuestName = guestName
                };
            }

            private Guid BookingId { get; set; }
            private string RoomType { get; set; }
            private DateTime StartDate { get; set; }
            private int NumberOfNights { get; set; }
            private string GuestName { get; set; }
        }
    }
}