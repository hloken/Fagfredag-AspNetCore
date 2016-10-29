using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
// ReSharper disable InconsistentNaming

namespace ForkusHotelApiIntegrationTests
{
    public static class TestUtils
    {
        private const string BookingServicePath = "api/booking";

        public static async Task<Guid> SetupABooking(
            this HttpClient httpClient, 
            string start = "2016-10-21T13:28:06.419Z", 
            int numberOfNights=3, 
            string roomType= "ForkusSuite")
        {
            var content = ToJsonStringContent(new
            {
                roomType = "ForkusSuite",
                startDate = start,
                numberOfNights = 3,
                guestName = "Kjell Lj0stad"
            });
            var response = await httpClient.PostAsync($"{BookingServicePath}/bookings", content);

            return (await GetBodyAsJson<RoomBookingResponseDto>(response)).bookingId;
        }

        public static StringContent ToJsonStringContent<T>(this T dtoObject)
        {
            var json = JsonConvert.SerializeObject(dtoObject);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            return content;
        }

        public static async Task<T> GetBodyAsJson<T>(this HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            var body = JsonConvert.DeserializeObject<T>(content);

            return body;
        }

        public class RoomBookingResponseDto
        {
            public Guid bookingId { get; set; }
        }

        public class AllRoomTypesDto
        {
            public string[] roomTypes { get; set; }
        }

        public class ErrorResponseDto
        {
            public string error {  get; set; }
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
}