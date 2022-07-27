using ClientLibrary.Transport;
using ClientLibrary.Utilities;
using ClientLibrary.Message;
using System;
using System.Windows.Forms;
using System.Diagnostics;

namespace ClientUI
{
    public partial class FormLogin : Form
    {
        public Communicator Communicator { get; private set; }
        public static ServerInfo[] servers = new ServerInfo[]
        {
            new ServerInfo("None", Constants.ServerDefaultIPv4, Constants.ServerDefaultPort),
            new ServerInfo("Mặc định", Constants.ServerDefaultIPv4, Constants.ServerDefaultPort)

        };
        public FormLogin()
        {
            InitializeComponent();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            string username = textUserName.Text;
            // Validate
            if (String.IsNullOrEmpty(username.Trim()))
            {
                MessageBox.Show("Tên đăng nhập không thể rỗng", "Tên đăng nhập không hợp lệ");
                return;
            }
            // Request Info from Server
            ServerInfo server = cmbChooseServer.SelectedItem as ServerInfo;
            if(server.ServerName == "None")
            {
                server = new ServerInfo("None", textBox1.Text, Constants.ServerDefaultPort);
            }
            // Connect and Login for user
            Connect(server);
            Communicator.Login(username);
            //if (Connect(server))
            //{
            //    Login(username);
            //}
        }
        private void ShowHomePage(string username, byte[] room_ids)
        {
            Communicator.ReceiveMessageSuccess -= Communicator_ReceiveMessageSuccess;
            Communicator.SendMessageSuccess -= Communicator_SendMessageSuccess;
            //Communicator.Disconnected -= Communicator_Disconnected;
            FormMain frmMain = new FormMain(username, room_ids, Communicator);
            this.Hide();
            frmMain.ShowDialog();
            this.Show();
            Communicator.ReceiveMessageSuccess += Communicator_ReceiveMessageSuccess;
            Communicator.SendMessageSuccess += Communicator_SendMessageSuccess;
            //Communicator.Disconnected += Communicator_Disconnected;
        }
        private bool Connect(ServerInfo server_info)
        {
            // Connect to Server
            Communicator = Communicator.GetInstance(server_info);
            
            if (Communicator == null)
            {
                MessageBox.Show($"Không thể kết nối tới Server.\nChi tiết: {Communicator.Errors}", "Lỗi kết nối");
                return false;
            }
            Communicator.ReceiveMessageSuccess += Communicator_ReceiveMessageSuccess;
            Communicator.SendMessageSuccess += Communicator_SendMessageSuccess;
            Communicator.Disconnected += Communicator_Disconnected;

            Communicator.Receiver.ReceiveAsync();
            return true;
        }

        private void ReceiveLoginResponse(LoginResponse response)
        {
            Invoke(new Action(() =>
            {
                if (response.Success)
                {
                    ShowHomePage(textUserName.Text, response.RoomIDs);
                }
                else
                {
                    MessageBox.Show(response.Message);
                }
            }));
        }
        private void Communicator_SendMessageSuccess(Sender sender, byte[] message)
        {
            Debug.WriteLine("*SEND* in LOGIN.");
        }

        private void Communicator_ReceiveMessageSuccess(Receiver receiver, byte[] message)
        {
            Debug.WriteLine("*RECEIVE* in LOGIN.");
            var response = ResponseMessage.GetResponseMessage(message);
            if (response == null)
            {
                Debug.WriteLine("LOGIN: Receive success but response is NULL");
            }
            else if (response is LoginResponse)
            {
                ReceiveLoginResponse(response as LoginResponse);
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
            Debug.WriteLine("*DISCONNECTED* in LOGIN.");
            //MessageBox.Show("Mất kết nối tới Server!");
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            Debug.WriteLine("*LOAD* in LOGIN");
            // Populate Data for UI
            cmbChooseServer.Items.AddRange(servers);
            cmbChooseServer.DisplayMember = nameof(ServerInfo.ServerName);
            cmbChooseServer.SelectedIndex = 0;
        }
    }
}
