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
    }
}