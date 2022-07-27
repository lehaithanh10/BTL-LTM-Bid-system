namespace ClientLibrary.Models
{
    public class RoomInfo
    {
        public static RoomInfo CreateRoomInfo(byte id, string name, string creatorname)
        {
            return new RoomInfo()
            {
                ID = id,
                Name = name,
                ItemsCount = 0,
                UsersCount = 1,
                CreatorName = creatorname,
                CurrentItem = null,
                CurrentHighestPrice = 0,
                CurrentHighestBidUserName = null
            };
        }
        public byte ID { get; set; }
        public string Name { get; set; }
        public string CreatorName { get; set; }
        public uint UsersCount { get; set; }
        public uint ItemsCount { get; set; }
        public ItemInfo CurrentItem { get; set; }
        public uint CurrentHighestPrice { get; set; }
        public string CurrentHighestBidUserName { get; set; }
    }
}
