using System;
using System.Linq;
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

        public bool IsCollision(DateTime startDate, int numberOfNights, string roomType)
        {
            return
                _bookingStore.Bookings.Any(
                    booking => roomType == booking.RoomType && DoBookingsIntersect(startDate, numberOfNights, booking));
        }

        public bool IsValidRoomType(string roomType)
        {
            return
                RoomType.AllRoomTypes.Any(rt => rt.Name == roomType);
        }

        private bool DoBookingsIntersect(DateTime startDate, int numberOfNights, BookingStore.Booking booking)
        {
            var endDate = startDate.AddDays(numberOfNights);
            var bookingEndDate = booking.StartDate.AddDays(booking.NumberOfNights);

            // Thank you http://stackoverflow.com/questions/13513932/algorithm-to-detect-overlapping-periods#answer-13513973
            var overlap = startDate < bookingEndDate && booking.StartDate < endDate;

            return overlap;
        }
    }
}