using ClientLibrary.Message;
using ClientLibrary.Models;
using ClientLibrary.Transport;
using ClientLibrary.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Net.Sockets;
using System.Windows.Forms;

namespace ClientUI
{
    public partial class FormMain : Form
    { 
        private readonly Color[] RoomColors = new Color[]
        {
            Color.Yellow, Color.Orange, Color.Tomato,
            Color.Violet, Color.Lime, Color.YellowGreen,
            Color.Khaki, Color.Aqua, Color.Magenta
        };
        private int _rooms_count = 0; // Number of Rooms on Client
        private List<byte> RoomIDs;
        public void UpdateListRoomsID(byte[] new_list_room_ids)
        {
            try
            {
                if (new_list_room_ids.Length <= RoomIDs.Count)
                    return;
                int start_diff = 0;
                for (; start_diff < RoomIDs.Count; start_diff++)
                    if (new_list_room_ids[start_diff] != RoomIDs[start_diff])
                        break;
                for (int i = start_diff; i < new_list_room_ids.Length; ++i)
                {
                    layoutRooms.Controls.Add(CreateRoomButton(new_list_room_ids[i]));
                }
            }
            catch { }
        }
        public string UserName { get; }
        public Communicator Communicator { get; }
        public FormMain()
        {
            InitializeComponent();
            UserName = "";
            Communicator = null;
        }
        public FormMain(string username, byte[] room_ids, Communicator communicator)
        {
            InitializeComponent();

            UserName = username;
            Communicator = communicator;
            RoomIDs = new List<byte>();
            RoomIDs.AddRange(room_ids);
        }

        private void Communicator_SendMessageSuccess(Sender sender, byte[] message)
        {
            Debug.WriteLine($"*SEND* in MAIN: {message.Length}");
        }

        private void Communicator_ReceiveMessageSuccess(Receiver receiver, byte[] message)
        {
            Debug.WriteLine("*RECEIVE* in MAIN.");
            var response = ResponseMessage.GetResponseMessage(message);
            if(response == null)
            {
                Debug.WriteLine("MAIN: Receive success but response is NULL");
            }
            if(response is CreateRoomResponse)
            {
                ReceiveCreateRoomResponse(response as CreateRoomResponse);
            }
            else if(response is JoinRoomResponse)
            {
                ReceiveJoinRoomResponse(response as JoinRoomResponse);
            }
            else if(response is UpdateNewRoomCreatedResponse)
            {
                ReceiveUpdateNewRoomCreatedResponse(response as UpdateNewRoomCreatedResponse);
            }
            else
            {
                Debug.WriteLine("MAIN: Receive success but response is UNEXPECTED");
            }
            // Done -> Continue Receive
            Communicator.Receiver.ReceiveAsync();
        }

        private void Communicator_Disconnected(System.Net.Sockets.Socket connector)
        {
            Debug.WriteLine("*DISCONNECTED* in MAIN.");
        }
        private void ReceiveUpdateNewRoomCreatedResponse(UpdateNewRoomCreatedResponse response)
        {
            Invoke(new Action(() =>
            {
                byte new_room_id = response.NewRoomID;
                RoomIDs.Add(new_room_id);
                layoutRooms.Controls.Add(CreateRoomButton(new_room_id));
            }));
        }
        private void ReceiveCreateRoomResponse(CreateRoomResponse response)
        {
            Invoke(new Action(() =>
            {
                if (response.Success)
                {
                    byte _created_room_id = response.CreatedRoomID;
                    Button new_room_button = CreateRoomButton(_created_room_id);
                    layoutRooms.Controls.Add(new_room_button);
                    RoomIDs.Add(_created_room_id);
                    ShowRoomPage(RoomInfo.CreateRoomInfo(_created_room_id, new_room_button.Text, UserName));
                }
                else
                {
                    MessageBox.Show(response.Message);
                }
            }));
        }
        private void ReceiveJoinRoomResponse(JoinRoomResponse response)
        {
            Invoke(new Action(() =>
            {
                if (response.Success)
                {
                    Button room_button = this.Tag as Button;
                    response.RoomInfo.ID = (byte)room_button.Tag;
                    response.RoomInfo.Name = room_button.Text;
                    ShowRoomPage(response.RoomInfo);
                }
                else
                {
                    MessageBox.Show(response.Message);
                }
            }));
        }
        /// <summary>
        /// Callback method when User click on Add Room button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateRoom_Click(object sender, EventArgs e)
        {
            Communicator.CreateRoom();
        }
       
        /// <summary>
        /// Create a Button so that user can click to join room
        /// </summary>
        /// <param name="room_id">The room id</param>
        /// <param name="name">The name of the room</param>
        /// <returns>Created Button</returns>
        private Button CreateRoomButton(byte room_id)
        {
            Button roomButton = new Button();
            roomButton.Name = $"button{room_id}";
            roomButton.Tag = room_id;
            roomButton.Text = $"Phòng đấu giá {room_id + 1}";
            roomButton.AutoEllipsis = true;
            roomButton.BackColor = RoomColors[_rooms_count % RoomColors.Length];
            roomButton.Font = new System.Drawing.Font("Times New Roman", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            roomButton.Margin = new System.Windows.Forms.Padding(30);
            roomButton.Size = new System.Drawing.Size(215, 108);
            roomButton.Click += RoomButton_Click;
            _rooms_count++;
            return roomButton;
        }
        /// <summary>
        /// Callback when user click on a Room
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RoomButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            byte room_id = (byte)button.Tag;
            this.Tag = button;
            Communicator.JoinRoom(room_id);
        }
        /// <summary>
        /// Perform hiding on current Form and show Room Page when User join room or create room
        /// </summary>
        /// <param name="roomInfo">The info of the room</param>
        private void ShowRoomPage(RoomInfo roomInfo)
        {
            Communicator.ReceiveMessageSuccess -= Communicator_ReceiveMessageSuccess;
            Communicator.SendMessageSuccess -= Communicator_SendMessageSuccess;
            //Communicator.Disconnected -= Communicator_Disconnected;
            FormRoom room = new FormRoom(roomInfo, Communicator);
            room.Text = $"Phòng đấu giá. Chào {UserName}!";
            this.Hide();
            room.ShowDialog();
            this.Show();
            Communicator.ReceiveMessageSuccess += Communicator_ReceiveMessageSuccess;
            Communicator.SendMessageSuccess += Communicator_SendMessageSuccess;
        }
        /// <summary>
        /// Callback method when current form prepares for presenting
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_Load(object sender, EventArgs e)
        {
            // Populate Data for UI

            if (UserName.Length > 30)
                labelHeader.Text = $"Chào mừng {UserName.Substring(0, 30)}... đã đến với ứng dụng Đấu giá";
            else
                labelHeader.Text = $"Chào mừng {UserName} đã đến với ứng dụng Đấu giá";

            if (RoomIDs.Count > 0)
            {
                List<Button> room_buttons = new List<Button>();
                foreach (byte room_id in RoomIDs)
                {
                    room_buttons.Add(CreateRoomButton(room_id));
                }
                layoutRooms.Controls.AddRange(room_buttons.ToArray());
            }
            // Set callback methods for Communicator.Events
            Communicator.ReceiveMessageSuccess += Communicator_ReceiveMessageSuccess;
            Communicator.SendMessageSuccess += Communicator_SendMessageSuccess;
            Communicator.Disconnected += Communicator_Disconnected;

            Communicator.Receiver.ReceiveAsync();
        }
        /// <summary>
        /// Callback method when current form already closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Communicator.SendMessageSuccess -= Communicator_SendMessageSuccess;
            Communicator.ReceiveMessageSuccess -= Communicator_ReceiveMessageSuccess;
            Communicator.Close();
        }
    }
}
