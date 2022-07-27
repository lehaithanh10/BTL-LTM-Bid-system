namespace ClientLibrary.Utilities
{
    public static class Constants
    {
        // Server
        public static string ServerDefaultIPv4        = "127.0.0.1";
        public static int ServerDefaultPort           = 5500;

        // Socket
        public static int SocketReceiveTimeout = 10000; // 1s
        public static int SocketSendTimeout = 10000; // 1s

        // Message Structure
        public static int MessageCodeFieldSize        = 1;
        public static int MessageLengthFieldSize      = 4;
        public static int MessageHeaderSize = MessageCodeFieldSize + MessageLengthFieldSize;

        // Data Field Size
        public static int UserNameSize = 100;
        public static int ItemNameSize = 100;
        public static int UserIDSize = 2;
        public static int ItemIDSize = 1;
        public static int RoomIDSize = 1;
            // _Quantity: 4
        public static int UserQuantitySize = 4;
        public static int ItemQuantitySize = 4;
            // Price: 4
        public static int ItemPriceSize = 4;

        // Content
        public static System.Text.Encoding Encoding = System.Text.Encoding.UTF8; // default encoding for text presentation. 
        public static int MinimumStepPriceForBid = 10000; // each times for bid, must raise at least 10000 than current highest price
        public static int BidTimesStep = 15;
    }
    public enum Endianness
    {
        LittleEndian    = 0,
        BigEndian       = 1,
        NetworkEndian   = BigEndian,
        HostEndian      = 2,
        Default         = LittleEndian
    }
    public enum RequestCode : byte
    {
        Login       = 110,
        CreateRoom  = 120,
        JoinRoom    = 130,
        AddItem     = 140,
        Bid         = 150,
        BuyNow      = 160,
        LeaveRoom   = 170,
        Undefined   = 255
    }
    public enum ResponseCode : byte
    {
        LoginSuccess            = 10,
        LoginNotAccount         = 11,
        LoginFail               = 19,

        CreateRoomSuccess       = 20,
        CreateRoomFail          = 29,

        JoinRoomSuccess         = 30,
        JoinRoomFail            = 39,

        AddItemSuccess          = 40, // sell item
        AddItemFail             = 49, 

        BidSuccess              = 50,
        BidFailCantBid          = 51,
        BidFailInvalidPrice     = 52,
        BidFail                 = 59,

        BuyNowSuccess           = 60,
        BuyNowFailCantBuy       = 61,
        BuyNowFailInvalidPrice  = 62,
        BuyNowFail              = 69,

        LeaveRoomSuccess        = 70,
        LeaveRoomFail           = 79,

        NotifyBidTime           = 80,
        NotifyNewRoom           = 82,
        NotifyUserQuantityChange = 83,
        NotifyItemQuantityChange = 84,
        NotifyBidResult         = 85,
        NotifyBuyNowResult      = 86,
        NotifyNewSessionStart   = 87,
        NotifyBidFinish         = 88,

        OperationFail           = 99
    }
}
