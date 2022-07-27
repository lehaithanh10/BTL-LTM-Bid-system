using System;
using ClientLibrary.Utilities;

namespace ClientLibrary.Message
{
    /// <summary>
    /// A general object that Client and Server transmit to each other to express Request or Response
    /// := Code (1) | Length (4) | Payload (Size depend on Length Field)
    /// </summary>
    public abstract class Message
    {
        /// <summary>
        /// The Message's Code. <see cref="ResponseCode"/> and <see cref="RequestCode"/>
        /// </summary>
        public byte Code { get; set; }
        /// <summary>
        /// The Message payload's length.
        /// </summary>
        public uint Length { get; set; }
        /// <summary>
        /// The Message's Content or Payload
        /// </summary>
        public byte[] Payload { get; set; }

        /// <summary>
        /// Create a Message instance from a byte stream which has Code, Length and Payload in it.
        /// </summary>
        /// <param name="bytestream">The byte stream has at least <see cref="Constants.MessageHeaderSize"/> bytes</param>
        /// <exception cref="ArgumentException">If the byte stream length is less than <see cref="Constants.MessageHeaderSize"/> bytes</exception>
        protected Message(byte[] bytestream)
        {
            if (bytestream.Length >= Constants.MessageHeaderSize)
            {
                Code = bytestream[0];
                Length = (uint)(bytestream.Length - Constants.MessageHeaderSize);
                Payload = new byte[Length];
                Array.Copy(bytestream, Constants.MessageHeaderSize, Payload, 0, Length);
            }
            else
                throw new ArgumentException($"The byte stream length is not valid (require at least {Constants.MessageHeaderSize} bytes)");
        }
        /// <summary>
        /// Create a Message instance from Code and Payload. The Length of the Message is Payload.Length or 0 if payload is null
        /// </summary>
        /// <param name="code">The code of the Message</param>
        /// <param name="payload">The payload of the Message. Can Null or Empty</param>
        protected Message(byte code, byte[] payload)
        {
            Code = code;
            Length = (uint)(payload == null ? 0 : payload.Length);
            Payload = payload;
        }
        protected Message() { }
        /// <summary>
        /// Convert Message instance to byte stream that can be send over the Socket
        /// </summary>
        public byte[] Serialize()
        {
            var result = new System.IO.MemoryStream((int)Length + Constants.MessageHeaderSize);
            result.WriteByte(Code);
            result.Write(ByteConverter.Convert(Length), 0, Constants.MessageLengthFieldSize);
            if (Payload != null)
                result.Write(Payload, 0, (int)Length);
            return result.ToArray();
        }
    }
}
