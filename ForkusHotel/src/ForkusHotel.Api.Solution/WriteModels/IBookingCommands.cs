using System;

namespace ForkusHotel.Api.Solution.WriteModels
{
    public interface IBookingCommands
    {
        Guid BookARoom(string roomType, DateTime startDate, int numberOfNights, string guestName);

        bool IsCollision(DateTime startDate, int numberOfNights, string roomType);

        bool IsValidRoomType(string roomType);
    }
}