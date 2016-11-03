using System;
using System.Collections.Generic;

namespace ForkusHotel.Api.Solution.Persistence
{
    internal class BookingStore
    {
        public List<Booking> Bookings { get; } = new List<Booking>();
        
        public class Booking
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

            internal bool PaymentConfirmed { get; set; }
            internal bool CheckedIn { get; set; }
            internal bool CheckedOut { get; set; }
        }
    }
}