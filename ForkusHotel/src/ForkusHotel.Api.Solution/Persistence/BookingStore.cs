using System;
using System.Collections.Generic;
using System.Linq;

namespace ForkusHotel.Api.Solution.Persistence
{
    internal class BookingStore : IBookingCommands, IBookingQueries
    {
        private List<Booking> _bookings = new List<Booking>();

        public Guid BookARoom(string roomType, DateTime startDate, int numberOfNights, string guestName)
        {
            var bookingId = Guid.NewGuid();

            _bookings.Add(Booking.Create(bookingId, roomType, startDate, numberOfNights, guestName));

            return bookingId;
        }

        public BookingListDto RetrieveAllBookings()
        {
            return new BookingListDto
            {
                bookings = (from booking in _bookings
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

            internal Guid BookingId { get; set; }
            internal string RoomType { get; set; }
            internal DateTime StartDate { get; set; }
            internal int NumberOfNights { get; set; }
            internal string GuestName { get; set; }
        }
    }
}