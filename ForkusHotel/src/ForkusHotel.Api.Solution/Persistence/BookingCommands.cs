using System;

namespace ForkusHotel.Api.Solution.Persistence
{
    public interface IBookingCommands
    {
        Guid NewBooking(string roomType, DateTime bookingDtoStartDate, int bookingDtoNumberOfNights, string bookingDtoGuestName);
    }
}