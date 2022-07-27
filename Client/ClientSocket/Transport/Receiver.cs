using ClientLibrary.Message;
using ClientLibrary.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientLibrary.Transport
{
    /// <summary>
    /// Handler for this delegate will be called when <see cref="Receiver"/> successfully receive a whole Message object
    /// </summary>
    /// <param name="receiver">The receiver</param>
    /// <param name="message">The received message raw (in bytestream)</param>
    public delegate void ReceiveMessageSuccessEventHandler(Receiver receiver, byte[] message);
    /// <summary>
    /// Used for Receiving with Socket
    /// </summary>
    public class Receiver
    {
        /// <summary>
        /// Errors on Receiving
        /// </summary>
        public string Errors { get; private set; }
        /// <summary>
        /// The socket used for Receiving
        /// </summary>
        private Socket Connector;
        /// <summary>
        /// Handler for this delegate will be called when the connection gracefully close.
        /// </summary>
        public event DisconnectedEventHandler Disconnected;
        /// <summary>
        /// Handler for this delegate will be called when <see cref="Receiver"/> successfully receive a whole Message
        /// </summary>
        public event ReceiveMessageSuccessEventHandler ReceiveMessageSuccess;
        public Receiver(Socket connector)
        {
            Connector = connector;
        }

        /// <summary>
        /// Receive a Message [Synchronous]. When receive success, invoke delegate <see cref="ReceiveMessageSuccess"/>
        /// </summary>
        /// <param name="message">Output received message</param>
        /// <returns>true if receive successfully a whole message. false otherwise, has errors or timeout</returns>
        public bool Receive(out byte[] message)
        {
            Debug.WriteLine("Receive Sync");
            ReceiveBuffer buffer = new ReceiveBuffer(Constants.MessageHeaderSize);
            if (Connector.Poll(Constants.SocketReceiveTimeout, SelectMode.SelectRead))
            {
                if (Receive(ref buffer, (uint)Constants.MessageHeaderSize))
                {
                    var message_bytes = buffer.MessageStream.ToArray();
                    uint payload_length = ByteConverter.ConvertBack(message_bytes.Skip(Constants.MessageCodeFieldSize).ToArray());
                    Debug.WriteLine($"Payload length: {payload_length}");
                    if (payload_length == 0)
                    {
                        message = message_bytes;
                        if (ReceiveMessageSuccess != null)
                        {
                            ReceiveMessageSuccess(this, message);
                        }
                        buffer.Dispose();
                        return true;
                    }
                    if (Connector.Poll(Constants.SocketReceiveTimeout, SelectMode.SelectRead))
                    {
                        if (Receive(ref buffer, payload_length))
                        {
                            message = buffer.MessageStream.ToArray();
                            if (ReceiveMessageSuccess != null)
                            {
                                ReceiveMessageSuccess(this, message);
                            }
                            buffer.Dispose();
                            return true;
                        }
                    }
                    else// timeout
                    {
                        Debug.WriteLine($"Timeout");
                        Errors = $"Timeout on receive message.\n";
                    }
                }
                else
                {
                    Debug.WriteLine($"Receive header fail {buffer.ReadBuffer.Length}/{buffer.ExpectedReceive}");
                }
            }
            else// timeout
            {
                Debug.WriteLine($"Timeout");
                Errors = $"Timeout on receive message.\n";
            }
            message = null;
            return false;
        }
        /// <summary>
        /// Begin receive a Message [Asynchronous]. When receive success, invoke delegate <see cref="ReceiveMessageSuccess"/>
        /// </summary>
        public void ReceiveAsync()
        {
            ReceiveBuffer buffer = new ReceiveBuffer(Constants.MessageHeaderSize);
            try
            {
                Connector.BeginReceive(buffer.ReadBuffer, 0, Constants.MessageHeaderSize, SocketFlags.None, ReceiveHeaderCallback, buffer);
            }
            catch
            {}
        }
        /// <summary>
         /// Receive length byte and write to a <see cref="ReceiveBuffer"/>
         /// </summary>
         /// <param name="buffer">Output, the received buffer</param>
         /// <param name="length">Number of bytes expected receive</param>
         /// <returns>true if receive success. false otherwise, has errors</returns>
        private bool Receive(ref ReceiveBuffer buffer, uint length)
        {
            buffer.ResetReadBuffer((int)length);
            try
            {
                while (buffer.ExpectedReceive > 0)
                {
                    int received_bytes = Connector.Receive(buffer.ReadBuffer);
                    if (received_bytes <= 0) // connection gracefully close
                    {
                        Debug.WriteLine("Receive bytes less than 0");
                        if (Disconnected != null)
                        {
                            Disconnected(Connector);
                        }
                        buffer.Dispose();
                        return false;
                    }
                    Debug.WriteLine($"Receive {received_bytes}/{buffer.ExpectedReceive}:");
                    foreach (var b in buffer.ReadBuffer)
                        Debug.Write(b + " ");
                    Debug.WriteLine("\n---------");
                    buffer.Write(received_bytes);
                    buffer.UpdateExpectedReceive(received_bytes);
                    Array.Clear(buffer.ReadBuffer, 0, buffer.ReadBuffer.Length);
                }
            }
            catch (SocketException se)
            {
                switch (se.SocketErrorCode)
                {
                    case SocketError.ConnectionAborted:
                    case SocketError.ConnectionReset:
                        if (Disconnected != null)
                        {
                            Disconnected(Connector);
                        }
                        break;
                }
                buffer.Dispose();
                return false;
            }
            catch (Exception ex)
            {
                Errors = ex.Message;
                buffer.Dispose();
                return false;
            }

            return true;
        }
        /// <summary>
        /// Callback method when receive success on Socket. BeginReceive is in <see cref="ReceiveAsync"/>
        /// </summary>
        /// <param name="result">State of receiving. A <see cref="ReceiveBuffer"/> object in this case</param>
        private void ReceiveHeaderCallback(IAsyncResult result)
        {
            Debug.WriteLine("Receive Header Async");
            var buffer = (ReceiveBuffer)result.AsyncState;
            try
            {
                int received_bytes = Connector.EndReceive(result);
                if (received_bytes <= 0) // connection gracefully close
                {
                    if (Disconnected != null)
                    {
                        Disconnected(Connector);
                    }
                    buffer.Dispose();
                    return;
                }

                if (received_bytes != Constants.MessageHeaderSize)
                {
                    for (int i = 0; i < received_bytes; ++i)
                        Debug.Write(buffer.ReadBuffer[i] + " ");
                    Debug.WriteLine("\n");
                    buffer.Dispose();
                    throw new Exception("Thông điệp nhận được có lỗi.");
                }

                buffer.Write(received_bytes);

            }
            catch (SocketException se)
            {
                switch (se.SocketErrorCode)
                {
                    case SocketError.ConnectionAborted:
                    case SocketError.ConnectionReset:
                        if (Disconnected != null)
                        {
                            Disconnected(Connector);
                        }
                        break;
                }
                buffer.Dispose();
                return;
            }
            catch (Exception ex)
            {
                Errors = ex.Message;
                buffer.Dispose();
                return;
            }
            // prepare for receiving payload
            int payload_length = (int)ByteConverter.ConvertBack(buffer.ReadBuffer.Skip(Constants.MessageCodeFieldSize).ToArray());
            if (payload_length > 0)
            {
                buffer.ResetReadBuffer(payload_length);
                // receive payload
                Connector.BeginReceive(buffer.ReadBuffer, 0, buffer.ReadBuffer.Length, SocketFlags.None, ReceivePayloadCallback, buffer);
            }
            else
            {
                Debug.WriteLine("Async Payload length = 0");
                if (ReceiveMessageSuccess != null)
                {
                    ReceiveMessageSuccess(this, buffer.MessageStream.ToArray());
                }

                buffer.Dispose();
            }
        }
        /// <summary>
        /// Callback method when receive success on Socket. 
        /// BeginReceive is in <see cref="ReceiveHeaderCallback(IAsyncResult)"/> after receiving successfully Message Header
        /// </summary>
        /// <param name="result">State of receiving. A <see cref="ReceiveBuffer"/> object in this case</param>
        private void ReceivePayloadCallback(IAsyncResult result)
        {
            Debug.WriteLine("Receive Payload Async");
            var buffer = (ReceiveBuffer)result.AsyncState;
            try
            {
                int received_bytes = Connector.EndReceive(result);
                if (received_bytes <= 0) // connection gracefully close
                {
                    if (Disconnected != null)
                    {
                        Disconnected(Connector);
                    }
                    buffer.Dispose();
                    return;
                }
                Debug.WriteLine($"Payload length: {received_bytes}/{buffer.ExpectedReceive}");
                buffer.Write(received_bytes);
                buffer.UpdateExpectedReceive(received_bytes);
            }
            catch (SocketException se)
            {
                switch (se.SocketErrorCode)
                {
                    case SocketError.ConnectionAborted:
                    case SocketError.ConnectionReset:
                        if (Disconnected != null)
                        {
                            Disconnected(Connector);
                        }
                        break;
                }
                buffer.Dispose();
                return;
            }
            catch (Exception ex)
            {
                Errors = ex.Message;
                buffer.Dispose();
                return;
            }

            if(buffer.ExpectedReceive > 0) // remaining bytes
            {
                Array.Clear(buffer.ReadBuffer, 0, buffer.ReadBuffer.Length);
                Connector.BeginReceive(buffer.ReadBuffer, 0, buffer.ExpectedReceive, SocketFlags.None, ReceivePayloadCallback, buffer);
                return;
            }

            if(ReceiveMessageSuccess != null)
            {
                ReceiveMessageSuccess(this, buffer.MessageStream.ToArray());
            }

            buffer.Dispose();
        }
    }

    /// <summary>
    /// Buffer used in Receiving Message from Socket's Receive Buffer
    /// </summary>
    public struct ReceiveBuffer
    {
        /// <summary>
        /// Buffer used for Reading Socket Receive Buffer
        /// </summary>
        public byte[] ReadBuffer;
        /// <summary>
        /// Number of Bytes expected to Receive
        /// </summary>
        public int ExpectedReceive;
        /// <summary>
        /// The Received Full Message (in bytes, not Message object)
        /// </summary>
        public MemoryStream MessageStream;
        /// <summary>
        /// Create a ReceiveBuffer Object
        /// </summary>
        /// <param name="size">Initial Size for the Buffer</param>
        public ReceiveBuffer(int size)
        {
            ExpectedReceive = size;
            ReadBuffer = new byte[size];
            MessageStream = new MemoryStream();
        }
        /// <summary>
        /// Reset the size of <see cref="ReadBuffer"/>
        /// </summary>
        /// <param name="size">Size for the Buffer</param>
        public void ResetReadBuffer(int size)
        {
            if (size < 0)
                size = int.MaxValue;
            ExpectedReceive = size;
            ReadBuffer = new byte[size];
        }
        /// <summary>
        /// Update value of <see cref="ExpectedReceive"/> when receive success
        /// </summary>
        /// <param name="receive_success">Number of bytes receive successfully</param>
        public void UpdateExpectedReceive(int receive_success)
        {
            ExpectedReceive -= receive_success;
        }
        /// <summary>
        /// Update <see cref="ReadBuffer"/> to <see cref="MessageStream"/>
        /// </summary>
        /// <param name="bytes">Number of bytes from beginning of <see cref="ReadBuffer"/> </param>
        public void Write(int bytes)
        {
            if (MessageStream != null && MessageStream.CanWrite)
            {
                Debug.Write($"Write {bytes}: ");
                foreach(var b in ReadBuffer)
                {
                    Debug.Write(b + " ");
                }
                Debug.WriteLine("\n------------");
                MessageStream.Write(ReadBuffer, 0, bytes);
            }
        }
        /// <summary>
        /// Free memory for ReceiveBuffer
        /// </summary>
        public void Dispose()
        {
            ReadBuffer = null;
            ExpectedReceive = 0;
            if (MessageStream != null)
            {
                if (MessageStream.CanWrite)
                    MessageStream.Close();
                MessageStream.Dispose();
            }
        }
    }

}
