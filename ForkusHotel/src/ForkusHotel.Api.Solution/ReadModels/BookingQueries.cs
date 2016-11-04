using System;
using ForkusHotel.Api.Solution.Persistence;
using System.Linq;
// ReSharper disable ClassNeverInstantiated.Global

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
                                guestName = booking.GuestName,
                            }).ToArray()
            };
        }

        public BookingDetailsDto RetrieveBookingDetails(Guid bookingId)
        {
            var dto = (from booking in _bookingStore.Bookings
                where booking.BookingId == bookingId
                select new BookingDetailsDto
                {
                    bookingId = booking.BookingId,
                    roomType = booking.RoomType,
                    startDate = booking.StartDate,
                    numberOfNights = booking.NumberOfNights,
                    guestName = booking.GuestName,
                    paymentConfirmed = booking.PaymentConfirmed,
                    checkedIn = booking.CheckedIn,
                    checkedOut = booking.CheckedOut
                }).SingleOrDefault();

            return dto;
        }

        public string[] RetrieveAllRoomTypes()
        {
            return (from roomType in RoomType.AllRoomTypes
                select roomType.Name).ToArray();
        }
    }
}