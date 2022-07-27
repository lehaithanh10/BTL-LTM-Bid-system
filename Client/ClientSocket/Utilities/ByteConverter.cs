using System;
using System.Linq;
using System.Text;

namespace ClientLibrary.Utilities
{
    public static class ByteConverter
    {
        /// <summary>
        /// Convert string object to byte array
        /// </summary>
        /// <param name="data">The string object</param>
        /// <param name="encoding">The Encoding object used for encode data in string. <see cref="Encoding"/></param>
        /// <returns>The converted byte array</returns>
        public static byte[] Convert(string data, Encoding encoding)
        {
            if (data == null)
                data = "";
            return encoding.GetBytes(data);
        }
        /// <summary>
        /// Convert string object to byte array with specific length [<paramref name="length"/> bytes]
        /// </summary>
        /// <param name="data">The string object</param>
        /// <param name="encoding">The Encoding object used for encode data in string. <see cref="Encoding"/></param>
        /// <param name="length">The length of the result</param>
        /// <returns>The converted byte array</returns>
        public static byte[] Convert(string data, Encoding encoding, int length)
        {
            if (data == null)
                data = "";
            byte[] result = new byte[length];
            byte[] data_bytes = encoding.GetBytes(data);
            Array.Copy(data_bytes, 0, result, 0, Math.Min(data_bytes.Length, length));
            return result;
        }
        /// <summary>
        /// Convert an unsigned integer number to byte array [4 bytes]
        /// </summary>
        /// <param name="data">The number</param>
        /// <param name="endianness">The endianness (BE/LE) indicates how the number can be presented as continuous bytes</param>
        /// <returns>The converted byte array</returns>
        public static byte[] Convert(uint data, Endianness endianness = Endianness.Default)
        {
            var result = BitConverter.GetBytes(data);
            if (endianness == Endianness.HostEndian)
                return result;
            if ((endianness == Endianness.LittleEndian) != BitConverter.IsLittleEndian)
                Array.Reverse(result);
            return result;
        }
        /// <summary>
        /// Convert an byte number to byte array [1 bytes]
        /// </summary>
        /// <param name="data">The number</param>
        /// <returns>The converted byte array</returns>
        public static byte[] Convert(byte data)
        {
            return new byte[] { data };
        }
        /// <summary>
        /// Convert a byte array to string object
        /// </summary>
        /// <param name="data">The byte array</param>
        /// <param name="encoding">The Encoding object used for encode data in string. <see cref="Encoding"/></param>
        /// <param name="trim_when_reach_0">true if want to consider (byte)0 is a character. false if want to remove following bytes when reach (byte)0</param>
        /// <returns>The converted string object</returns>
        public static string ConvertBack(byte[] data, Encoding encoding, bool trim_when_reach_0 = true)
        {
            int null_index = Array.IndexOf(data, (byte)0);
            if (null_index != -1)
                return encoding.GetString(data.Take(null_index).ToArray());
            return encoding.GetString(data);
        }
        /// <summary>
        /// Convert a byte array (from index 'start' in source array and has 'length' bytes) to string object
        /// </summary>
        /// <param name="data">The source byte array</param>
        /// <param name="start">Offset index in source byte array</param>
        /// <param name="length">Number of bytes from <paramref name="start"/></param>
        /// <param name="encoding">The Encoding object used for encode data in string. <see cref="Encoding"/></param>
        /// <returns>The converted string object</returns>
        public static string ConvertBack(byte[] data, int start, int length, Encoding encoding, bool trim_when_reach_0 = true)
        {
            int null_index = Array.IndexOf(data, (byte)0, start, length);
            if (null_index != -1)
                return encoding.GetString(data.Skip(start).Take(null_index - start).ToArray());
            return encoding.GetString(data.Skip(start).Take(length).ToArray());
        }
        /// <summary>
        /// Convert a byte array to an unsigned integer number
        /// </summary>
        /// <param name="data">The source byte array</param>
        /// <param name="endianness">The endianness (BE/LE) indicates how the number can be presented as continuous bytes</param>
        /// <returns>The converted number</returns>
        public static uint ConvertBack(byte[] data, Endianness endianness = Endianness.Default)
        {
            if (endianness != Endianness.HostEndian)
            {
                bool isConvertLittleEndian = (endianness == Endianness.LittleEndian);
                if (isConvertLittleEndian != BitConverter.IsLittleEndian)
                {
                    // TODO: May change the arguments
                    Array.Reverse(data);
                }
            }
            return BitConverter.ToUInt32(data, 0);
        }
        /// <summary>
        /// Convert a byte array (from index 'start' in source array and has 'length' bytes) to an unsigned integer number
        /// </summary>
        /// <param name="data">The source byte array</param>
        /// <param name="start">Offset index in source byte array</param>
        /// <param name="length">Number of bytes from <paramref name="start"/></param>
        /// <param name="endianness">The endianness (BE/LE) indicates how the number can be presented as continuous bytes</param>
        /// <returns>The converted number</returns>
        public static uint ConvertBack(byte[] data, int start, int length, Endianness endianness = Endianness.Default)
        {
            byte[] temp = data.Skip(start).Take(length).ToArray();
            if (endianness != Endianness.HostEndian)
            {
                bool isConvertLittleEndian = (endianness == Endianness.LittleEndian);
                if (isConvertLittleEndian != BitConverter.IsLittleEndian)
                {
                    Array.Reverse(temp);
                }
            }
            return BitConverter.ToUInt32(temp, 0);
        }
    }
}
