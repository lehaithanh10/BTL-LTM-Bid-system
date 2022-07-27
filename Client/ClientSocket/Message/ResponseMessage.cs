using System;
using ClientLibrary.Models;
using ClientLibrary.Utilities;

namespace ClientLibrary.Message
{
    public class ResponseMessage : Message
    {
        protected ResponseMessage(byte[] bytestream) : base(bytestream) { }

        public static T GetResponseMessage<T>(byte[] bytestream) where T : ResponseMessage 
        {
            ResponseMessage response = GetResponseMessage(bytestream);
            if (response == null)
                return null;
            return response as T;
        }
        public static ResponseMessage GetResponseMessage(byte[] bytestream)
        {
            try
            {
                byte response_code = bytestream[0];
                RequestCode request_code = RequestCode.Undefined;
                string message = null;
                bool success = true;

                switch ((ResponseCode)response_code)
                {
                    #region operation response SUCCESS
                    case ResponseCode.LoginSuccess:
                        request_code = RequestCode.Login; break;
                    case ResponseCode.CreateRoomSuccess:
                        request_code = RequestCode.CreateRoom; break;
                    case ResponseCode.JoinRoomSuccess:
                        request_code = RequestCode.JoinRoom; break;
                    case ResponseCode.LeaveRoomSuccess:
                        request_code = RequestCode.LeaveRoom; break;
                    case ResponseCode.AddItemSuccess:
                        request_code = RequestCode.AddItem; break;
                    case ResponseCode.BidSuccess:
                        request_code = RequestCode.Bid; break;
                    case ResponseCode.BuyNowSuccess:
                        request_code = RequestCode.BuyNow; break;
                    #endregion

                    #region operation response FAIL
                    case ResponseCode.LoginNotAccount:
                        success = false;
                        message = "Tên tài khoản không tồn tại trên Server";
                        request_code = RequestCode.Login; break;
                    case ResponseCode.LoginFail:
                        success = false;
                        message = "Đăng nhập thất bại.";
                        request_code = RequestCode.Login; break;
                    case ResponseCode.CreateRoomFail:
                        success = false;
                        message = "Tạo phòng thất bại.";
                        request_code = RequestCode.CreateRoom; break;
                    case ResponseCode.JoinRoomFail:
                        success = false;
                        message = "Không thể tham gia vào phòng.";
                        request_code = RequestCode.JoinRoom; break;
                    case ResponseCode.LeaveRoomFail:
                        success = false;
                        message = "Không thể rời phòng.";
                        request_code = RequestCode.LeaveRoom; break;
                    case ResponseCode.AddItemFail:
                        success = false;
                        message = "Thêm vật phẩm thất bại";
                        request_code = RequestCode.AddItem; break;
                    case ResponseCode.BidFailCantBid:
                        success = false;
                        message = "Bạn là người bán vật phẩm này và Không thể đấu giá nó.";
                        request_code = RequestCode.Bid; break;
                    case ResponseCode.BidFailInvalidPrice:
                        success = false;
                        message = "Giá trị bạn đấu giá không hợp lệ.";
                        request_code = RequestCode.Bid; break;
                    case ResponseCode.BidFail:
                        success = false;
                        message = "Đấu giá thất bại.";
                        request_code = RequestCode.Bid; break;
                    case ResponseCode.BuyNowFailCantBuy:
                        success = false;
                        message = "Bạn là người bán vật phẩm này và Không thể mua nó.";
                        request_code = RequestCode.BuyNow; break;
                    case ResponseCode.BuyNowFailInvalidPrice:
                        success = false;
                        message = "Giá trị bạn mua không hợp lệ.";
                        request_code = RequestCode.BuyNow; break;
                    case ResponseCode.BuyNowFail:
                        success = false;
                        message = "Mua ngay thất bại.";
                        request_code = RequestCode.BuyNow; break;
                    case ResponseCode.OperationFail:
                        success = false;
                        message = "Thao tác yêu cầu không hợp lệ";
                        break;
                    #endregion

                    #region update response
                    case ResponseCode.NotifyBidTime:
                        return new UpdateBidTimesResponse(bytestream);
                    case ResponseCode.NotifyUserQuantityChange:
                        return new UpdateUserQuantityResponse(bytestream);
                    case ResponseCode.NotifyItemQuantityChange:
                        return new UpdateItemQuantityResponse(bytestream);
                    case ResponseCode.NotifyNewRoom:
                        return new UpdateNewRoomCreatedResponse(bytestream);
                    case ResponseCode.NotifyBidResult:
                        return new UpdateBidResultResponse(bytestream);
                    case ResponseCode.NotifyBuyNowResult:
                        return new UpdateBuyNowResultResponse(bytestream);
                    case ResponseCode.NotifyNewSessionStart:
                        return new UpdateNewSessionStartResponse(bytestream);
                    case ResponseCode.NotifyBidFinish:
                        return new UpdateBidFinishResponse(bytestream);
                    #endregion

                    default:
                        return null;
                }

                // operation request's response return here
                return OperationResponse.GetOperationResponse(bytestream, request_code, success, message);
            }
            catch
            {
                return null;
            }
        }
    }
    
    #region Operation Responses
    public class LoginResponse : OperationResponse
    {
        public byte[] RoomIDs { get; }
        internal LoginResponse(byte[] bytestream, bool success, string message) 
            : base(bytestream, RequestCode.Login, success, message)
        {
            if (Success)
            {
                if (Length != 0)
                    RoomIDs = Payload.Clone() as byte[];
                else
                    RoomIDs = new byte[0];
            }
            else
            {
                RoomIDs = null;
            }
        }
    }
    public class CreateRoomResponse : OperationResponse
    {
        public byte CreatedRoomID { get; }
        internal CreateRoomResponse(byte[] bytestream, bool success, string message) 
            : base(bytestream, RequestCode.CreateRoom, success, message)
        {
            CreatedRoomID = 0;
            if (Success)
            {
                if (Length != Constants.RoomIDSize)
                    throw new ArgumentException();
                else
                    CreatedRoomID = Payload[0];
            }
        }
    }
    public class LeaveRoomResponse : OperationResponse
    {
        public byte[] RoomIDs { get; }
        internal LeaveRoomResponse(byte[] bytestream, bool success, string message)
            : base(bytestream, RequestCode.LeaveRoom, success, message)
        {
            if (Success)
            {
                if (Length != 0)
                    RoomIDs = Payload.Clone() as byte[];
                else
                    RoomIDs = new byte[0];
            }
            else
            {
                RoomIDs = null;
            }
        }
    }
    public class JoinRoomResponse : OperationResponse
    {
        public RoomInfo RoomInfo { get; set; }
        internal JoinRoomResponse(byte[] bytestream, bool success, string message) 
            : base(bytestream, RequestCode.JoinRoom, success, message)
        {
            if (Success)
            {
                RoomInfo = new RoomInfo();
                
                int start = 0;

                // Set Room Creator
                RoomInfo.CreatorName = ByteConverter.ConvertBack(Payload, start, Constants.UserNameSize, Constants.Encoding);
                start += Constants.UserNameSize;
                // Set Room Users Count
                RoomInfo.UsersCount = ByteConverter.ConvertBack(Payload, start, Constants.UserQuantitySize);
                start += Constants.UserQuantitySize;
                // Set Room Items Count
                RoomInfo.ItemsCount = ByteConverter.ConvertBack(Payload, start, Constants.ItemQuantitySize);
                start += Constants.ItemQuantitySize;
                // Set Current Highest Bid UserName
                RoomInfo.CurrentHighestBidUserName = ByteConverter.ConvertBack(Payload, start, Constants.UserNameSize, Constants.Encoding);
                start += Constants.UserNameSize;
                
                string item_name = ByteConverter.ConvertBack(Payload, start, Constants.ItemNameSize, Constants.Encoding).Trim();
                start += Constants.ItemNameSize;
                if (String.IsNullOrEmpty(item_name))
                {
                    RoomInfo.CurrentItem = null;
                    RoomInfo.CurrentHighestPrice = 0;
                }
                else
                {
                    // Set Current Highest Price
                    RoomInfo.CurrentHighestPrice = ByteConverter.ConvertBack(Payload, start, Constants.ItemPriceSize);
                    start += Constants.ItemPriceSize;

                    // Extract Current Item Initial Price
                    uint initial_price = ByteConverter.ConvertBack(Payload, start, Constants.ItemPriceSize);
                    start += Constants.ItemPriceSize;

                    // Extract Current Item Buy Now Price
                    uint buy_now_price = ByteConverter.ConvertBack(Payload, start, Constants.ItemPriceSize);
                    start += Constants.ItemPriceSize;

                    // Extract Current Item Description
                    string description = ByteConverter.ConvertBack(Payload, start, (int)(Length - start), Constants.Encoding);

                    RoomInfo.CurrentItem = ItemInfo.CreateItem(item_name, description, initial_price, buy_now_price);
                }
            }
            else
            {
                RoomInfo = null;
            }
        }
    }
    public class AddItemResponse : OperationResponse
    {
        internal AddItemResponse(byte[] bytestream, bool success, string message) 
            : base(bytestream, RequestCode.AddItem, success, message) { }
    }
    public class BidResponse : OperationResponse
    {
        internal BidResponse(byte[] bytestream, bool success, string message) 
            : base(bytestream, RequestCode.Bid, success, message) { }
    }
    public class BuyNowResponse : OperationResponse
    {
        internal BuyNowResponse(byte[] bytestream, bool success, string message)
            : base(bytestream, RequestCode.BuyNow, success, message) { }
    }
    public class OperationResponse : ResponseMessage
    {
        public bool Success { get; }
        public RequestCode RequestCode { get; }
        public string Message { get; set; }
        protected OperationResponse(byte[] bytestream, RequestCode request_code, bool success, string message) : base(bytestream)
        {
            RequestCode = request_code;
            Success = success;
            Message = message;
        }
        public static OperationResponse GetOperationResponse(byte[] bytestream, RequestCode request_code, bool success, string message)
        {
            switch (request_code)
            {
                case RequestCode.Login:
                    return new LoginResponse(bytestream, success, message);
                case RequestCode.CreateRoom:
                    return new CreateRoomResponse(bytestream, success, message);
                case RequestCode.JoinRoom:
                    return new JoinRoomResponse(bytestream, success, message);
                case RequestCode.LeaveRoom:
                    return new LeaveRoomResponse(bytestream, success, message);
                case RequestCode.AddItem:
                    return new AddItemResponse(bytestream, success, message);
                case RequestCode.Bid:
                    return new BidResponse(bytestream, success, message);
                case RequestCode.BuyNow:
                    return new BuyNowResponse(bytestream, success, message);
                default:
                    return new OperationResponse(bytestream, RequestCode.Undefined, success, message);
            }
        }
    }
    #endregion

    #region Update Responses
    public abstract class UpdateResponse : ResponseMessage
    {
        public UpdateResponse(byte[] bytestream) : base(bytestream)
        {

        }
    }
    public class UpdateBidTimesResponse : UpdateResponse
    {
        public int Times { get; set; }
        internal UpdateBidTimesResponse(byte[] bytestream) : base(bytestream)
        {
            if (Length == 1)
                Times = (int)Payload[0];
            else
                Times = 0;
        }
    }
    public class UpdateUserQuantityResponse : UpdateResponse
    {
        public uint NewUserQuantity { get; set; }
        public UpdateUserQuantityResponse(byte[] bytestream) : base(bytestream)
        {
            if (Length != Constants.UserQuantitySize)
                throw new ArgumentException();
            NewUserQuantity = ByteConverter.ConvertBack(Payload);
        }
    }
    public class UpdateItemQuantityResponse : UpdateResponse
    {
        public uint NewItemQuantity { get; set; }
        public UpdateItemQuantityResponse(byte[] bytestream) : base(bytestream)
        {
            if (Length != Constants.ItemQuantitySize)
                throw new ArgumentException();
            NewItemQuantity = ByteConverter.ConvertBack(Payload);
        }
    }
    public class UpdateBidResultResponse : UpdateResponse
    {
        public string UserName { get; set; }
        public uint NewPrice { get; set; }
        public UpdateBidResultResponse(byte[] bytestream) : base(bytestream)
        {
            int start = 0;

            // Set User Name
            UserName = ByteConverter.ConvertBack(Payload, start, Constants.UserNameSize, Constants.Encoding);
            start += Constants.UserNameSize;
            // Set New Price
            NewPrice = ByteConverter.ConvertBack(Payload, start, Constants.ItemPriceSize);
        }
    }
    public class UpdateBuyNowResultResponse : UpdateResponse
    {
        public string WinUserName { get; set; }
        public UpdateBuyNowResultResponse(byte[] bytestream) : base(bytestream)
        {
            if (Length != Constants.UserNameSize)
                throw new ArgumentException();
            WinUserName = ByteConverter.ConvertBack(Payload, Constants.Encoding);
        }
    }
    public class UpdateNewRoomCreatedResponse : UpdateResponse
    {
        public byte NewRoomID { get; set; }
        public UpdateNewRoomCreatedResponse(byte[] bytestream) : base(bytestream)
        {
            if (Length != Constants.RoomIDSize)
                throw new ArgumentException();
            NewRoomID = Payload[0];
        }
    }
    public class UpdateNewSessionStartResponse : UpdateResponse
    {
        public ItemInfo NewItem { get; set; }
        public UpdateNewSessionStartResponse(byte[] bytestream) : base(bytestream)
        {
            int start = 0;
            // Extract Current Item Name
            string name = ByteConverter.ConvertBack(Payload, start, Constants.ItemNameSize, Constants.Encoding).Trim();
            if (String.IsNullOrEmpty(name))
            {
                NewItem = null;
            }
            else
            {
                start += Constants.ItemNameSize;
                // Extract Current Item Initial Price
                uint initial_price = ByteConverter.ConvertBack(Payload, start, Constants.ItemPriceSize);
                start += Constants.ItemPriceSize;
                // Extract Current Item Buy Now Price
                uint buy_now_price = ByteConverter.ConvertBack(Payload, start, Constants.ItemPriceSize);
                start += Constants.ItemPriceSize;
                // Extract Current Item Description
                string description = ByteConverter.ConvertBack(Payload, start, (int)(Length - start), Constants.Encoding);

                NewItem = ItemInfo.CreateItem(name, description, initial_price, buy_now_price);
            }
        }
    }
    public class UpdateBidFinishResponse : UpdateResponse
    {
        internal UpdateBidFinishResponse(byte[] bytestream) : base(bytestream)
        { }
    }
    #endregion
}
