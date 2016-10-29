using System;
using ForkusHotel.Api.Solution.Persistence;
using System.Linq;

namespace ForkusHotel.Api.Solution.ReadModels
{
    internal class BookingQueries : IBookingQueries
    {
        private readonly BookingStore _bookingStore;

        public BookingQueries(BookingStore bookingStore)
        {
            _bookingStore = bookingStore;
        }

        public BookingListDto RetrieveAllBookings()
        {
            return new BookingListDto
            {
                bookings = (from booking in _bookingStore.Bookings
                            select new BookingListItemDto
                            {
                                bookingId = booking.BookingId,
                                roomType = booking.RoomType,
                                startDate = booking.StartDate,
                                numberOfNights = booking.NumberOfNights,
                                guestName = booking.GuestName
                            }).ToArray()
            };
        }

        public bool IsCollision(DateTime startDate, int numberOfNights, string roomType)
        {
            return
                _bookingStore.Bookings.Any(
                    booking => roomType == booking.RoomType && DoesBookingsIntersect(startDate, numberOfNights, booking));
        }

        private bool DoesBookingsIntersect(DateTime startDate, int numberOfNights, BookingStore.Booking booking)
        {
            var endDate = startDate.AddDays(numberOfNights);
            var bookingEndDate = booking.StartDate.AddDays(booking.NumberOfNights);

            // Thank you http://stackoverflow.com/questions/13513932/algorithm-to-detect-overlapping-periods#answer-13513973
            var overlap = startDate < bookingEndDate && booking.StartDate < endDate;
            
            return overlap;
        }
    }
}