using ClientLibrary.Message;
using ClientLibrary.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Sockets;

namespace ClientLibrary.Transport
{
    /// <summary>
    /// Handler for this delegate will be called when the connection gracefully close.
    /// </summary>
    /// <param name="connector">The socket used for communication</param>
    public delegate void DisconnectedEventHandler(Socket connector);

    public class Communicator
    {
        /// <summary>
        /// Handler for this delegate will be called when the connection gracefully close.
        /// </summary>
        public event DisconnectedEventHandler Disconnected
        {
            add { Receiver.Disconnected += value; Sender.Disconnected += value; }
            remove { Receiver.Disconnected -= value; Sender.Disconnected -= value; }
        }
        /// <summary>
        /// Handler for this delegate will be called when <see cref="Receiver"/> successfully receive a whole Message
        /// </summary>
        public event ReceiveMessageSuccessEventHandler ReceiveMessageSuccess
        {
            add { Receiver.ReceiveMessageSuccess += value; }
            remove { Receiver.ReceiveMessageSuccess -= value; }
        }
        /// <summary>
        /// Handler for this delegate will be called when <see cref="Sender"/> successfully send a whole Message.
        /// </summary>
        public event SendMessageSuccessEventHandler SendMessageSuccess
        {
            add { Sender.SendMessageSuccess += value; }
            remove { Sender.SendMessageSuccess -= value; }
        }
        /// <summary>
        /// Address of Server and other information
        /// </summary>
        public ServerInfo ServerInfo { get; set; }
        /// <summary>
        /// The socket used for Receiving
        /// </summary>
        public Socket Connector { get; private set; }
        /// <summary>
        /// Receive message from Socket
        /// </summary>
        public Receiver Receiver { get; private set; }
        /// <summary>
        /// Send message to Socket
        /// </summary>
        public Sender Sender { get; private set; }
        /// <summary>
        /// Errors on Sending or Receiving
        /// </summary>
        public static string Errors { get; private set; }

        private static Communicator instance = null;
        private Communicator(ServerInfo serverInfo)
        {
            ServerInfo = serverInfo;
        }
        public static Communicator GetInstance(ServerInfo serverInfo)
        {
            if (instance == null || instance.ServerInfo != serverInfo)
            {
                try
                {
                    instance = new Communicator(serverInfo);

                    Socket connector = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    instance.Connector = connector;
                    instance.Receiver = new Receiver(connector);
                    instance.Sender = new Sender(connector);
                    // TODO:
                    connector.Connect(serverInfo.IPv4Address, serverInfo.Port);
                }
                catch(SocketException se)
                {
                    Errors = se.Message;
                    Debug.WriteLine(se.ToString());
                    instance = null;
                }
            }
            return instance;
        }
        /// <summary>
        /// [Synchronous] Login with specific username.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool Login(string username)
        {
            return Sender.Send(RequestMessage.CreateLoginRequestMessage(username).Serialize());
        }
        /// <summary>
        /// [Synchronous]
        /// </summary>
        /// <returns></returns>
        //public CreateRoomResponse CreateRoom()
        //{
        //    if (Sender.Send(RequestMessage.CreateCreateRoomRequestMessage().Serialize()))
        //    {
        //        if (Receiver.Receive(out byte[] message))
        //        {
        //            var response = ResponseMessage.GetResponseMessage(message);
        //            if (response != null)
        //                return response as CreateRoomResponse;
        //        }
        //    }
        //    return null;
        //}
        public bool CreateRoom()
        {
            return (Sender.Send(RequestMessage.CreateCreateRoomRequestMessage().Serialize()));
        }

        /// <summary>
        /// [Synchronous]
        /// </summary>
        /// <returns></returns>

        public bool JoinRoom(byte room_id)
        {
            return Sender.Send(RequestMessage.CreateJoinRoomRequestMessage(room_id).Serialize());
        }
        public bool AddItem(byte room_id, ItemInfo item)
        {
            return (Sender.Send(RequestMessage.CreateAddItemRequestMessage(room_id, item).Serialize()));
        }
        public bool LeaveRoom(byte room_id)
        {
            return (Sender.Send(RequestMessage.CreateLeaveRoomRequestMessage(room_id).Serialize()));
        }
        public bool Bid(byte room_id, uint new_price)
        {
            return (Sender.Send(RequestMessage.CreateBidRequestMessage(room_id, new_price).Serialize()));
        }
        public bool BuyNow(byte room_id, uint buy_now_price)
        {
            return (Sender.Send(RequestMessage.CreateBuyNowRequestMessage(room_id, buy_now_price).Serialize()));
        }

        /// <summary>
        /// Close Connection
        /// </summary>
        public void Close()
        {
            Connector.Close();
            Errors = null;
            instance = null;
        }
        /*
        private bool SendMustDone(byte[] data)
        {
            int start = 0;
            int size = data.Length;
            int sent_succ = 0;
            try
            {
                while (sent_succ < size)
                {
                    sent_succ = Connector.Send(data, start, size, SocketFlags.None);

                    start += sent_succ;
                    size -= sent_succ;
                }
            }
            catch (SocketException ex)
            {
                Errors = $"{ex.StackTrace}: {ex.Message}\n";
                return false;
            }
            return true;
        }
        private bool ReceiveMustDone(byte[] buffer, int offset = 0, int size = 0)
        {
            if (size == 0)
                size = buffer.Length;
            int start = 0;
            int recv_succ = 0;
            try
            {
                while (recv_succ < size)
                {
                    recv_succ = Connector.Receive(buffer, offset + start, size, SocketFlags.None);

                    start += recv_succ;
                    size -= recv_succ;
                }
            }
            catch (SocketException ex)
            {
                Errors = $"{ex.StackTrace}: {ex.Message}\n";
                return false;
            }
            return true;
        }
        private bool SendRequest(RequestMessage message)
        {
            return SendMustDone(message.Serialize());
        }
        private ResponseMessage ReceiveResponse()
        {
            var stream = new System.IO.MemoryStream();
            byte[] header = new byte[Constants.MessageHeaderSize];
            if (!ReceiveMustDone(header))
                return null;

            stream.Write(header, 0, Constants.MessageHeaderSize);
            uint length = ByteConverter.ConvertBack(header, Constants.MessageCodeFieldSize, Constants.MessageLengthFieldSize, Endianness.HostEndian);

            if (length == 0)
                return ResponseMessage.GetResponseMessage(stream.ToArray());
            if (length > 0)
            {
                byte[] payload = new byte[length];
                if (ReceiveMustDone(payload))
                {
                    stream.Write(payload, 0, (int)length);
                    return ResponseMessage.GetResponseMessage(stream.ToArray());
                }
            }

            return null;
        }
        public LoginResponse Login(string username)
        {
            Debug.WriteLine($"LOGIN on Thread: {Thread.CurrentThread.ManagedThreadId}");
            if (SendRequest(RequestMessage.CreateLoginRequestMessage(username))){
                var response = ReceiveResponse();
                if (response != null)
                    return response as LoginResponse;
            }
            return null;
        }
        public CreateRoomResponse CreateRoom()
        {
            Debug.WriteLine($"CREATEROOM on Thread: {Thread.CurrentThread.ManagedThreadId}");
            if (SendRequest(RequestMessage.CreateCreateRoomRequestMessage()))
            {
                var response = ReceiveResponse();
                if (response != null)
                    return response as CreateRoomResponse;
            }
            return null;
        }
        public JoinRoomResponse JoinRoom(byte roomid)
        {
            Debug.WriteLine($"JOINROOM on Thread: {Thread.CurrentThread.ManagedThreadId}");
            if (SendRequest(RequestMessage.CreateJoinRoomRequestMessage(roomid)))
            {
                var response = ReceiveResponse();
                if (response != null)
                    return response as JoinRoomResponse;
            }
            return null;
        }
        public AddItemResponse AddItem(Models.ItemInfo item)
        {
            Debug.WriteLine($"ADDITEM on Thread: {Thread.CurrentThread.ManagedThreadId}");
            if (SendRequest(RequestMessage.CreateAddItemRequestMessage(item)))
            {
                var response = ReceiveResponse();
                if (response != null)
                    return response as AddItemResponse;
            }
            return null;
        }
        public BidResponse Bid(uint price)
        {
            Debug.WriteLine($"BID on Thread: {Thread.CurrentThread.ManagedThreadId}");
            if (SendRequest(RequestMessage.CreateBidRequestMessage(price)))
            {
                var response = ReceiveResponse();
                if (response != null)
                    return response as BidResponse;
            }
            return null;
        }
        public BuyNowResponse BuyNow()
        {
            Debug.WriteLine($"BUYNOW on Thread: {Thread.CurrentThread.ManagedThreadId}");
            if (SendRequest(RequestMessage.CreateBuyNowRequestMessage()))
            {
                var response = ReceiveResponse();
                if (response != null)
                    return response as BuyNowResponse;
            }
            return null;
        }
        */
    }
}
