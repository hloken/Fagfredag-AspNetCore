using System;
// ReSharper disable InconsistentNaming

namespace ForkusHotel.Api.Solution.ReadModels
{
    public interface IBookingQueries
    {
        BookingListDto RetrieveAllBookings();
        BookingDetailsDto RetrieveBookingDetails(Guid bookingId);
    }

    // TODO: since these classes are exposed on the wire they should be made explicit contracts
    public class BookingDetailsDto
    {
        public Guid bookingId { get; set; }
        public string roomType { get; set; }
        public DateTime startDate { get; set; }
        public int numberOfNights { get; set; }
        public string guestName { get; set; }
        public bool paymentConfirmed { get; set; }
        public bool checkedIn { get; set; }
        public bool checkedOut { get; set; }
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