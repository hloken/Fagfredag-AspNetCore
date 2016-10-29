using System;
using ForkusHotel.Api.Solution.Persistence;

namespace ForkusHotel.Api.Solution.WriteModels
{
    internal class BookingCommands : IBookingCommands
    {
        private readonly BookingStore _bookingStore;

        public BookingCommands(BookingStore bookingStore)
        {
            _bookingStore = bookingStore;
        }

        public Guid BookARoom(string roomType, DateTime startDate, int numberOfNights, string guestName)
        {
            var bookingId = Guid.NewGuid();

            _bookingStore.Bookings.Add(BookingStore.Booking.Create(bookingId, roomType, startDate, numberOfNights, guestName));

            return bookingId;
        }
    }
}