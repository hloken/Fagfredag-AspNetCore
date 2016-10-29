using System;
// ReSharper disable InconsistentNaming

namespace ForkusHotel.Api.Solution.ReadModels
{
    public interface IBookingQueries
    {
        BookingListDto RetrieveAllBookings();
        bool IsCollision(DateTime startDate, int numberOfNights, string roomType);
    }

    public class BookingListDto
    {
        public BookingListItemDto[] bookings
        {
            get; set;
        }
    }

    public class BookingListItemDto
    {
        public Guid bookingId
        {
            get; set;
        }
        public string roomType
        {
            get; set;
        }
        public DateTime startDate
        {
            get; set;
        }
        public int numberOfNights
        {
            get; set;
        }
        public string guestName
        {
            get; set;
        }
    }
}