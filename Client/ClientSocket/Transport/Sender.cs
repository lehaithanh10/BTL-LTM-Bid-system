using ClientLibrary.Utilities;
using System;
using System.Diagnostics;
using System.Net.Sockets;

namespace ClientLibrary.Transport
{
    /// <summary>
    /// Handler for this delegate will be called when <see cref="Sender"/> successfully send a whole Message.
    /// </summary>
    /// <param name="sender">The sender</param>
    /// <param name="message">The message raw (in bytestream)</param>
    public delegate void SendMessageSuccessEventHandler(Sender sender, byte[] message);
    /// <summary>
    /// Used for Sending with Socket
    /// </summary>
    public class Sender
    {
        /// <summary>
        /// Errors on Sending
        /// </summary>
        public string Errors { get; private set; }
        /// <summary>
        /// The socket used for sending
        /// </summary>
        private Socket Connector;
        /// <summary>
        /// Handler for this delegate will be called when the connection gracefully close.
        /// </summary>
        public event DisconnectedEventHandler Disconnected;
        /// <summary>
        /// Handler for this delegate will be called when <see cref="Sender"/> successfully send a whole Message.
        /// </summary>
        public event SendMessageSuccessEventHandler SendMessageSuccess;
        public Sender(Socket connector)
        {
            Connector = connector;
        }
        /// <summary>
        /// Send a Message [Synchronous]. When send success, invoke delegate <see cref="SendMessageSuccess"/>
        /// </summary>
        /// <param name="message">The message raw</param>
        public bool Send(byte[] message)
        {
            if(message != null)
                return Send(new SendBuffer(message));
            return true;
        }
        /// <summary>
        /// Send a buffer [Synchronous]. When send success, invoke delegate <see cref="SendMessageSuccess"/>
        /// </summary>
        /// <param name="buffer">The SendBuffer</param>
        public bool Send(SendBuffer buffer)
        {
            int sent_succ = 0;
            try
            {
                while (buffer.ExpectedSend > 0)
                {
                    if (Connector.Poll(Constants.SocketSendTimeout, SelectMode.SelectWrite))
                    {
                        sent_succ = Connector.Send(buffer.WriteBuffer, buffer.Start, buffer.ExpectedSend, SocketFlags.None);
                        Debug.WriteLine($"Sent {sent_succ}/{buffer.ExpectedSend}");
                    }
                    else
                    {
                        Debug.WriteLine($"Timeout");
                        Errors = "Timeout on sending message.";
                        return false;
                    }

                    buffer.UpdateExpectedSend(sent_succ);
                }
            }
            catch (SocketException ex)
            {
                Errors = ex.Message;
                return false;
            }

            if (SendMessageSuccess != null)
            {
                SendMessageSuccess(this, buffer.WriteBuffer);
            }

            return true;
        }
        /// <summary>
        /// Begin send a Message [Asynchronous]. When send success, invoke delegate <see cref="SendMessageSuccess"/>
        /// </summary>
        /// <param name="message">The message raw</param>
        public void SendAsync(byte[] message)
        {
            if(message != null)
            {
                SendAsync(new SendBuffer(message));
            }
        }
        /// <summary>
        /// Begin send a buffer [Asynchronous]. When send success, invoke delegate <see cref="SendMessageSuccess"/>
        /// </summary>
        /// <param name="buffer">The SendBuffer</param>
        private void SendAsync(SendBuffer buffer)
        {
            Connector.BeginSend(buffer.WriteBuffer, buffer.Start, buffer.ExpectedSend, SocketFlags.None, SendCallback, buffer);
        }
        /// <summary>
        /// Callback method when send success on Socket. 
        /// BeginSend is in <see cref="SendAsync(SendBuffer)"/>
        /// </summary>
        /// <param name="result">State of sending. The <see cref="SendBuffer"/> in this case</param>
        private void SendCallback(IAsyncResult result)
        {

            SendBuffer buffer = (SendBuffer)result.AsyncState;
            try
            {
                int sent_bytes = Connector.EndSend(result);
                if(sent_bytes <= 0)
                {
                    if (Disconnected != null)
                    {
                        Disconnected(Connector);
                    }
                    buffer.Dispose();
                    return;
                }

                buffer.UpdateExpectedSend(sent_bytes);
            }
            catch(Exception ex)
            {
                Errors = ex.Message;
                return;
            }

            if (buffer.ExpectedSend > 0)
            {
                SendAsync(buffer);
                return;
            }

            if(SendMessageSuccess != null)
            {
                SendMessageSuccess(this, buffer.WriteBuffer);
            }

            buffer.Dispose();
        }
    }
    /// <summary>
    /// Buffer used in Sending Message to Socket's Send Buffer
    /// </summary>
    public struct SendBuffer
    {
        /// <summary>
        /// Buffer used for Writing Socket Send Buffer
        /// </summary>
        public byte[] WriteBuffer;
        /// <summary>
        /// Number of Bytes expected to Send
        /// </summary>
        public int ExpectedSend;
        /// <summary>
        /// The offset of first byte will send in <see cref="WriteBuffer"/>
        /// </summary>
        public int Start;

        /// <summary>
        /// Create a SendBuffer Object
        /// </summary>
        /// <param name="buffer">The content want to send</param>
        /// <param name="start">The offset of first byte will send</param>
        /// <param name="length">Number of bytes will send. 0 for used <paramref name="buffer"/>.Length</param>
        public SendBuffer(byte[] buffer, int start = 0, int length = 0)
        {
            WriteBuffer = buffer;
            if (length == 0)
                ExpectedSend = WriteBuffer.Length;
            else
                ExpectedSend = Math.Min(WriteBuffer.Length, length);
            Start = start;
        }
        /// <summary>
        /// Update value of <see cref="ExpectedSend"/> when send success
        /// </summary>
        /// <param name="send_success">Number of bytes send successfully</param>
        public void UpdateExpectedSend(int send_success)
        {
            ExpectedSend -= send_success;
            Start += send_success;
        }
        
        /// <summary>
        /// Free memory for SendBuffer
        /// </summary>
        public void Dispose()
        {
            WriteBuffer = null;
            ExpectedSend = 0;
            Start = 0;
        }
    }
}
