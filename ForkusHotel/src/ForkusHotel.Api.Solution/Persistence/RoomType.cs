// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace ForkusHotel.Api.Solution.Persistence
{
    public class RoomType
    {
        public static readonly RoomType Single = new RoomType("Single");
        public static readonly RoomType Double = new RoomType("Double");
        public static readonly RoomType Twin = new RoomType("Twin");
        public static readonly RoomType DeluxeDouble = new RoomType("DeluxeDouble");
        public static readonly RoomType JuniorSuite = new RoomType("JuniorSuite");
        public static readonly RoomType Suite = new RoomType("Suite"); 
        public static readonly RoomType ForkusSuite = new RoomType("ForkusSuite");

        public static RoomType[] AllRoomTypes => new[] {Single, Double, Twin, DeluxeDouble, JuniorSuite, Suite, ForkusSuite};

        public RoomType(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}