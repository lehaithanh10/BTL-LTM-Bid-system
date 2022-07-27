using ClientLibrary.Message;
using ClientLibrary.Models;
using ClientLibrary.Transport;
using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace ClientUI
{
    public partial class FormRoom : Form
    {
        /// <summary>
        /// The object used for Sending and Receiving with connected Server
        /// </summary>
        public Communicator Communicator { get; }
        /// <summary>
        /// The current room infomation detail
        /// </summary>
        public RoomInfo RoomInfo { get; }
        public int _bid_time = -1;
        public FormRoom()
        {
            InitializeComponent();
        }
        public FormRoom(RoomInfo room_info, Communicator communicator)
        {
            InitializeComponent();
            RoomInfo = room_info;
            Communicator = communicator;
        }
        
        #region Communicator Callback
        /// <summary>
        /// Callback method when the sender of the communicator send a whole message
        /// </summary>
        /// <param name="sender">The <see cref="Sender"/> used for sending</param>
        /// <param name="message">The sended message raw</param>
        private void Communicator_SendMessageSuccess(Sender sender, byte[] message)
        {
            Debug.WriteLine("*SEND* in ROOM.");
        }
        /// <summary>
        /// Callback method when the connection of the Socket has been close gracefully
        /// </summary>
        /// <param name="connector"></param>
        private void Communicator_Disconnected(System.Net.Sockets.Socket connector)
        {
            Debug.WriteLine("*DISCONNECTED* in ROOM.");
            //MessageBox.Show("Mất kết nối tới Server!");
        }
        /// <summary>
        /// Callback method when the receiver of the communicator receive a whole message
        /// </summary>
        /// <param name="receiver">The <see cref="Receiver"/> used for receiving</param>
        /// <param name="message">The received message raw</param>
        private void Communicator_ReceiveMessageSuccess(Receiver receiver, byte[] message)
        {
            Debug.WriteLine("*RECEIVE* in ROOM.");
            var response = ResponseMessage.GetResponseMessage(message);
            if (response == null)
            {
                Debug.WriteLine("ROOM: Receive success but response is NULL");
            }
            else if (response is OperationResponse)
            {
                if (response is AddItemResponse)
                    ReceiveAddItemResponse(response as AddItemResponse);
                else if (response is BidResponse)
                    ReceiveBidResponse(response as BidResponse);
                else if (response is BuyNowResponse)
                    ReceiveBuyNowResponse(response as BuyNowResponse);
                else if (response is LeaveRoomResponse)
                    ReceiveLeaveRoomResponse(response as LeaveRoomResponse);
                else
                {
                    Debug.WriteLine("ROOM: Receive success but response is UNEXPECTED");
                }
            }
            else if (response is UpdateResponse)
            {
                if (response is UpdateUserQuantityResponse)
                    ReceiveUpdateUserQuantityResponse(response as UpdateUserQuantityResponse);
                else if (response is UpdateItemQuantityResponse)
                    ReceiveUpdateItemQuantityResponse(response as UpdateItemQuantityResponse);
                else if (response is UpdateBuyNowResultResponse)
                    ReceiveUpdateBuyNowResultResponse(response as UpdateBuyNowResultResponse);
                else if (response is UpdateBidResultResponse)
                    ReceiveUpdateBidResultResponse(response as UpdateBidResultResponse);
                else if (response is UpdateNewSessionStartResponse)
                    ReceiveUpdateNewSessionStartResponse(response as UpdateNewSessionStartResponse);
                else if (response is UpdateBidFinishResponse)
                    ReceiveUpdateBidFinishResponse(response as UpdateBidFinishResponse);
                else if (response is UpdateBidTimesResponse)
                    ReceiveUpdateBidTimesResponse(response as UpdateBidTimesResponse);
                else
                {
                    Debug.WriteLine("ROOM: Receive success but response is UNEXPECTED");
                }
            }

            // Done -> Continue listening
            Communicator.Receiver.ReceiveAsync();
        }
        #endregion

        #region Receive Response Handler
        /// <summary>
        /// Callback method when receive a whole <see cref="UpdateBidTimesResponse"/>
        /// </summary>
        /// <param name="response">The Response</param>
        private void ReceiveUpdateBidTimesResponse(UpdateBidTimesResponse response)
        {
            Invoke(new Action(() =>
            {
                labelTime.Visible = true;
                _bid_time = ClientLibrary.Utilities.Constants.BidTimesStep;
                uint current_price = (RoomInfo.CurrentHighestPrice == 0 ? RoomInfo.CurrentItem.InitialPrice : RoomInfo.CurrentHighestPrice);
                labelServerNotification.Text = $"Server: {String.Format("{0:n0}", current_price)} lần thứ {response.Times}";
            }));
        }
        /// <summary>
        /// Callback method when receive a whole <see cref="UpdateBidFinishResponse"/>
        /// </summary>
        /// <param name="response">The Response</param>
        private void ReceiveUpdateBidFinishResponse(UpdateBidFinishResponse response)
        {
            Invoke(new Action(() =>
            {
                labelTime.Visible = false;
                if (RoomInfo.CurrentHighestBidUserName == null)
                {
                    if(RoomInfo.CurrentItem != null)
                        labelServerNotification.Text = $"Vật phẩm {RoomInfo.CurrentItem.Name} không được bán.";
                }
                else
                {
                    MessageBox.Show($"{RoomInfo.CurrentHighestBidUserName} trả giá cao nhất và thắng vật phẩm {(RoomInfo.CurrentItem == null ? "" : RoomInfo.CurrentItem.Name)}.", "Thông báo");

                    //labelServerNotification.Text = $"{RoomInfo.CurrentHighestBidUserName} trả giá cao nhất và thắng vật phẩm {(RoomInfo.CurrentItem == null ? "" : RoomInfo.CurrentItem.Name)}.";
                }
            }));
        }
        /// <summary>
        /// Callback method when receive a whole <see cref="UpdateBuyNowResultResponse"/>
        /// </summary>
        /// <param name="response">The Response</param>
        private void ReceiveUpdateBuyNowResultResponse(UpdateBuyNowResultResponse response)
        {
            Invoke(new Action(() =>
            {
                labelTime.Visible = false;
                //labelServerNotification.Text = $"{response.WinUserName} đã mua thành công vật phẩm {(RoomInfo.CurrentItem == null ? "" : RoomInfo.CurrentItem.Name)}.";

                MessageBox.Show($"{response.WinUserName} đã mua thành công vật phẩm {(RoomInfo.CurrentItem == null ? "" : RoomInfo.CurrentItem.Name)}.", "Thông báo");
            }));
        }
        /// <summary>
        /// Callback method when receive a whole <see cref="UpdateBidResultResponse"/>
        /// </summary>
        /// <param name="response">The Response</param>
        private void ReceiveUpdateBidResultResponse(UpdateBidResultResponse response)
        {
            Invoke(new Action(() =>
            {
                labelTime.Visible = false;

                RoomInfo.CurrentHighestPrice = response.NewPrice;
                RoomInfo.CurrentHighestBidUserName = response.UserName;
                labelHighestPrice.Text = String.Format("{0:n0}", RoomInfo.CurrentHighestPrice);
                labelHighestBuyer.Text = $"{RoomInfo.CurrentHighestBidUserName} trả giá cao nhất cho vật phẩm hiện tại";
                labelServerNotification.Text = null;
            }));
        }
        /// <summary>
        /// Callback method when receive a whole <see cref="UpdateNewSessionStartResponse"/>
        /// </summary>
        /// <param name="response">The Response</param>
        private void ReceiveUpdateNewSessionStartResponse(UpdateNewSessionStartResponse response)
        {
            Invoke(new Action(() =>
            {
                labelTime.Visible = false;
                ItemInfo item = response.NewItem;
                RoomInfo.ItemsCount--;
                labelItemsCount.Text = $"{RoomInfo.ItemsCount}";
                if (item == null)
                {
                    labelServerNotification.Text = $"Hiện tại chưa có vật phẩm để đấu giá";
                    panelSessionInfo.Visible = false;
                }
                else
                {
                    RoomInfo.CurrentItem = item;
                    RoomInfo.CurrentHighestPrice = 0;
                    RoomInfo.CurrentHighestBidUserName = null;
                    PopulateUIData(RoomInfo);
                }
            }));
        }
        /// <summary>
        /// Callback method when receive a whole <see cref="UpdateItemQuantityResponse"/>
        /// </summary>
        /// <param name="response">The Response</param>
        private void ReceiveUpdateItemQuantityResponse(UpdateItemQuantityResponse response)
        {
            Invoke(new Action(() =>
            {
                RoomInfo.ItemsCount = response.NewItemQuantity;
                labelItemsCount.Text = "" + response.NewItemQuantity;
            }));
        }
        /// <summary>
        /// Callback method when receive a whole <see cref="UpdateUserQuantityResponse"/>
        /// </summary>
        /// <param name="response">The Response</param>
        private void ReceiveUpdateUserQuantityResponse(UpdateUserQuantityResponse response)
        {
            Invoke(new Action(() =>
            {
                RoomInfo.UsersCount = response.NewUserQuantity;
                labelUsersCount.Text = "" + response.NewUserQuantity;
            }));
        }
        /// <summary>
        /// Callback method when receive a whole <see cref="LeaveRoomResponse"/>
        /// </summary>
        /// <param name="response">The Response</param>
        private void ReceiveLeaveRoomResponse(LeaveRoomResponse response)
        {
            if (response.Success)
            {
                Invoke(new Action(() =>
                {
                    (this.Parent as FormMain).UpdateListRoomsID(response.RoomIDs);
                    this.Dispose();
                }));
            }
            else
            {
                Debug.WriteLine($"LeaveRoomResponse Fail on Room");
                labelServerNotification.Text = $"Yêu cầu rời phòng bị từ chối";
            }
        }
        /// <summary>
        /// Callback method when receive a whole <see cref="BuyNowResponse"/>
        /// </summary>
        /// <param name="response">The Response</param>
        private void ReceiveBuyNowResponse(BuyNowResponse response)
        {
            Invoke(new Action(() =>
            {
                if (response.Success)
                {
                    labelTime.Visible = false;
                    MessageBox.Show($"Chúc mừng bạn đã mua thành công vật phẩm {(RoomInfo.CurrentItem == null ? "" : RoomInfo.CurrentItem.Name)}.");
                }
                else
                {
                    labelServerNotification.Text = response.Message;
                }
            }));
        }
        /// <summary>
        /// Callback method when receive a whole <see cref="BidResponse"/>
        /// </summary>
        /// <param name="response">The Response</param>
        private void ReceiveBidResponse(BidResponse response)
        {
            Invoke(new Action(() =>
            {
                if (response.Success)
                {
                    labelTime.Visible = false;

                    RoomInfo.CurrentHighestPrice = (uint)numPrice.Value;
                    RoomInfo.CurrentHighestBidUserName = "Bạn";
                    labelHighestPrice.Text = String.Format("{0:n0}", RoomInfo.CurrentHighestPrice);
                    labelHighestBuyer.Text = $"{RoomInfo.CurrentHighestBidUserName} trả giá cao nhất cho vật phẩm hiện tại";
                    labelServerNotification.Text = null;
                }
                else
                {
                    labelServerNotification.Text = response.Message;
                }
            }));
        }
        /// <summary>
        /// Callback method when receive a whole <see cref="AddItemResponse"/>
        /// </summary>
        /// <param name="response">The Response</param>
        private void ReceiveAddItemResponse(AddItemResponse response)
        {
            if (response.Success)
            {
                Invoke(new Action(() =>
                {
                    RoomInfo.ItemsCount += 2;
                    labelItemsCount.Text = "" + RoomInfo.ItemsCount;
                }));
            }
            else
            {
                labelServerNotification.Text = $"Thêm vật phẩm thất bại.";
            }
        }
        #endregion

        #region Form Events Callback
        /// <summary>
        /// Callback method when User click on AddItem Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAddItem_Click(object sender, EventArgs e)
        {
            FormAddItem formAdd = new FormAddItem();
            if(formAdd.ShowDialog() == DialogResult.OK)
            {
                ItemInfo[] created_items = formAdd.FR_Items;
                if (created_items == null)
                    return;

                // TODO: Send Items to Server on another thread
                new Thread(() =>
                {
                    foreach (var item in created_items)
                    {
                        Communicator.AddItem(RoomInfo.ID, item);
                    }
                }).Start();
            }
        }
        /// <summary>
        /// Callback method when User click on Buy Now Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonBuyNow_Click(object sender, EventArgs e)
        {
            new Thread(() =>
            {
                Communicator.BuyNow(RoomInfo.ID, RoomInfo.CurrentItem.BuyNowPrice);
            }).Start();
        }
        /// <summary>
        /// Callback method when User click on Bid Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonBid_Click(object sender, EventArgs e)
        {
            new Thread(() =>
            {
                Communicator.Bid(RoomInfo.ID, (uint)numPrice.Value);
            }).Start();
        }
        /// <summary>
        /// Callback method when User click on LeaveRoom Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonLeaveRoom_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Callback method when current form prepares for presenting
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormRoom_Load(object sender, EventArgs e)
        {
            // Populate data for UI
            labelRoomName.Text = RoomInfo.Name;
            labelCreator.Text = RoomInfo.CreatorName;
            labelItemsCount.Text = RoomInfo.ItemsCount.ToString();
            labelUsersCount.Text = RoomInfo.UsersCount.ToString();
            if(RoomInfo.CurrentItem == null)
            {
                labelServerNotification.Text = $"Hiện tại chưa có vật phẩm để đấu giá";
                panelSessionInfo.Visible = false;
            }
            else
            {
                PopulateUIData(RoomInfo);
            }

            // Set callback methods for Communicator.Events
            Communicator.ReceiveMessageSuccess += Communicator_ReceiveMessageSuccess;
            Communicator.SendMessageSuccess += Communicator_SendMessageSuccess;
            Communicator.Disconnected += Communicator_Disconnected;

            Communicator.Receiver.ReceiveAsync();

            timerRoom.Start();
        }
        /// <summary>
        /// Callback method when current form close finish
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormRoom_FormClosed(object sender, FormClosedEventArgs e)
        {
            Communicator.SendMessageSuccess -= Communicator_SendMessageSuccess;
            Communicator.ReceiveMessageSuccess -= Communicator_ReceiveMessageSuccess;
        }
        /// <summary>
        /// Callback method when the timer of current form tick (each 1s)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SessionTimer_Tick(object sender, EventArgs e)
        {
            if (_bid_time > 0)
            {
                int minutes = _bid_time / 60;
                int seconds = _bid_time - minutes * 60;
                labelTime.Text = $"{minutes.ToString("D2")}:{seconds.ToString("D2")}";
                --_bid_time;
            }
            else
            {
                labelTime.Visible = false;
            }
        }
        /// <summary>
        /// Callback method when current form prepares for closing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormRoom_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(MessageBox.Show("Bạn có chắc muốn rời phòng hiện tại.", "Xác nhận", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                Communicator.LeaveRoom(RoomInfo.ID);
            }
            else
            {
                e.Cancel = true;
            }
        }
        #endregion

        #region Utilities

        /// <summary>
        /// Populate value for some labels and other controls on current form with the specific room info
        /// </summary>
        /// <param name="room_info">The Room Info</param>
        private void PopulateUIData(RoomInfo room_info)
        {
            labelServerNotification.Text = null;
            panelSessionInfo.Visible = true;

            ItemInfo item = room_info.CurrentItem;
            labelItemName.Text = item.Name;
            tipRoom.SetToolTip(labelItemName, item.Description);
            labelBuyNowPrice.Text = String.Format("{0:n0}", item.BuyNowPrice);
            uint current_highest_price = (room_info.CurrentHighestPrice == 0 ? item.InitialPrice : room_info.CurrentHighestPrice);
            labelHighestPrice.Text = String.Format("{0:n0}", current_highest_price);
            numPrice.Minimum = current_highest_price;
            numPrice.Value = current_highest_price + 10000;
            if (String.IsNullOrEmpty(room_info.CurrentHighestBidUserName))
            {
                labelHighestBuyer.Text = null;
            }
            else
            {
                labelHighestBuyer.Text = $"{room_info.CurrentHighestBidUserName} trả giá cao nhất cho vật phẩm hiện tại";
            }
        }

        #endregion

    }
}
