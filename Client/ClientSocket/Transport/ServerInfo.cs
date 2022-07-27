namespace ClientLibrary.Transport
{
    public class ServerInfo
    {
        public string ServerName { get; set; }
        public string IPv4Address { get; set; }
        public int Port { get; set; }

        public ServerInfo(string serverName, string iPv4Address, int port)
        {
            ServerName = serverName;
            IPv4Address = iPv4Address;
            Port = port;
        }
    }
}
