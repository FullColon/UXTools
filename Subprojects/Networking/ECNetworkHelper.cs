using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Net.Sockets;

namespace EC.Networking
{
    public static class ECNetworkHelper
    {
        /// <summary>
        /// Get a UTF-8 string from server buffer.
        /// </summary>
        /// <param name="response">buffer from server</param>
        /// <returns>UTF-8 representation of data.</returns>
        public static string NetworkBufferToUTF8String(byte[] response)
        {
            return Encoding.UTF8.GetString(response);
        }

        /// <summary>
        /// Get an ASCII string from server response.
        /// </summary>
        /// <param name="response">response from server</param>
        /// <returns>ASCII representation of data.</returns>
        public static string NetworkBufferToASCIIString(byte[] response)
        {
            return Encoding.ASCII.GetString(response);
        }

        /// <summary>
        /// Convert a network buffer to a object type.
        /// </summary>
        /// <typeparam name="T">Type of object to deserialize to.</typeparam>
        /// <param name="buffer">Network buffer</param>
        /// <returns>Deserialized Object</returns>
        public static T NetworkBufferToObject<T>(byte[] buffer)
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            ms.Write(buffer, 0, buffer.Length);
            ms.Seek(0, SeekOrigin.Begin);
            T obj = (T)bf.Deserialize(ms);
            return obj;
        }

        /// <summary>
        /// Get a network buffer from a UTF-8 string.
        /// </summary>
        /// <param name="s">string to convert</param>
        /// <returns>Network buffer</returns>
        public static byte[] UTF8StringToNetworkBuffer(string s)
        {
            return Encoding.UTF8.GetBytes(s);
        }

        /// <summary>
        /// Get a network buffer from a UTF-8 string.
        /// </summary>
        /// <param name="s"></param>
        /// <returns>Network buffer.</returns>
        public static byte[] ASCIIStringToNetworkBuffer(string s)
        {
            return Encoding.ASCII.GetBytes(s);
        }

        /// <summary>
        /// Convert an object to a network buffer.
        /// </summary>
        /// <param name="obj">Object to convert</param>
        /// <returns>Network buffer</returns>
        public static byte[] ObjectToNetworkBuffer(object obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, obj);
            return ms.ToArray();
        }

        /// <summary>
        /// Check a sockets connection status.
        /// </summary>
        /// <param name="socket">Socket to check</param>
        /// <returns>Is connected</returns>
        public static bool IsSocketConnected(Socket socket)
        {
            try
            {
                return !(socket.Poll(1, SelectMode.SelectRead) && socket.Available == 0);
            }
            catch (SocketException) { return false; }
        }
    }
}
