using ClientLibrary.Utilities;

namespace ClientLibrary.Message
{
    /// <summary>
    /// A Message that use to send from Client to Server to Request
    /// </summary>
    public class RequestMessage : Message
    {
        private RequestMessage(RequestCode code, byte[] payload) :
            base((byte)code, payload)
        { }

        /// <summary>
        /// Create a Login Message that request to login.
        /// </summary>
        /// <param name="username">The UserName of the user want to login</param>
        /// <returns>The Message</returns>
        public static RequestMessage CreateLoginRequestMessage(string username)
        {
            return new RequestMessage(RequestCode.Login, 
                ByteConverter.Convert(username, Constants.Encoding, Constants.UserNameSize));
        }
        /// <summary>
        /// Create a CreateRoom Message that request Server create a room for BID
        /// </summary>
        /// <returns>The Message</returns>
        public static RequestMessage CreateCreateRoomRequestMessage()
        {
            return new RequestMessage(RequestCode.CreateRoom, null);
        }
        /// <summary>
        /// Create a JoinRoom Message that request to join a BID room
        /// </summary>
        /// <param name="room_id">The ID of the Room want to join</param>
        /// <returns>The Message</returns>
        public static RequestMessage CreateJoinRoomRequestMessage(byte room_id)
        {
            return new RequestMessage(RequestCode.JoinRoom, ByteConverter.Convert(room_id));
        }
        /// <summary>
        /// Create a LeaveRoom Message that request to leave a BID room
        /// </summary>
        /// <param name="room_id">The ID of the Room want to leave. Current Room</param>
        /// <returns>The Message</returns>
        public static RequestMessage CreateLeaveRoomRequestMessage(byte room_id)
        {
            return new RequestMessage(RequestCode.LeaveRoom, ByteConverter.Convert(room_id));
        }
        /// <summary>
        /// Create a AddItem Message that request to add an ItemInfo object to current BID Items List on Server
        /// </summary>
        /// <param name="room_id">The ID of the Room</param>
        /// <param name="item">The ItemInfo object want to add</param>
        /// <returns>The Message</returns>
        public static RequestMessage CreateAddItemRequestMessage(byte room_id, Models.ItemInfo item)
        {
            var payload = new System.IO.MemoryStream();
            byte[] item_name_bytes = ByteConverter.Convert(item.Name, Constants.Encoding, Constants.ItemNameSize);
            byte[] initial_price_bytes = ByteConverter.Convert(item.InitialPrice);
            byte[] buy_now_price_bytes = ByteConverter.Convert(item.BuyNowPrice);
            byte[] description_bytes = ByteConverter.Convert(item.Description, Constants.Encoding);

            payload.Write(ByteConverter.Convert(room_id), 0, Constants.RoomIDSize);
            payload.Write(item_name_bytes, 0, Constants.ItemNameSize);
            payload.Write(initial_price_bytes, 0, Constants.ItemPriceSize);
            payload.Write(buy_now_price_bytes, 0, Constants.ItemPriceSize);
            payload.Write(description_bytes, 0, description_bytes.Length);

            return new RequestMessage(RequestCode.AddItem, payload.ToArray());
        }
        /// <summary>
        /// Create a Bid Message that request to offer new price for current BID item
        /// </summary>
        /// <param name="room_id">The ID of the Room</param>
        /// <param name="new_price">New offer price</param>
        /// <returns>The Message</returns>
        public static RequestMessage CreateBidRequestMessage(byte room_id, uint new_price)
        {
            var payload = new System.IO.MemoryStream();
            payload.Write(ByteConverter.Convert(room_id), 0, Constants.RoomIDSize);
            payload.Write(ByteConverter.Convert(new_price), 0, Constants.ItemPriceSize);
            return new RequestMessage(RequestCode.Bid, payload.ToArray());
        }
        /// <summary>
        /// Create a BuyNow Message that request to buy current item with the price specified by the ItemInfo on Server
        /// </summary>
        /// <param name="room_id">The ID of the Room</param>
        /// /// <param name="price">The buy price</param>
        /// <returns>The Message</returns>
        public static RequestMessage CreateBuyNowRequestMessage(byte room_id, uint price)
        {
            var payload = new System.IO.MemoryStream();
            payload.Write(ByteConverter.Convert(room_id), 0, Constants.RoomIDSize);
            payload.Write(ByteConverter.Convert(price), 0, Constants.ItemPriceSize);
            return new RequestMessage(RequestCode.BuyNow, payload.ToArray());
        }
    }
}
