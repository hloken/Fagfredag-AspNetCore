using System;

namespace ForkusHotel.Api.Solution.Persistence
{
    public interface IBookingCommands
    {
        Guid BookARoom(string roomType, DateTime bookingDtoStartDate, int bookingDtoNumberOfNights, string bookingDtoGuestName);
    }
}