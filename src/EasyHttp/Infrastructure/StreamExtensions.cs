using System.IO;
using System.Text;

namespace EasyHttp.Infrastructure
{
    /// <summary>
    /// A class to handle streaming behaviour
    /// </summary>
    public static class StreamExtensions
    {
        public static void WriteString(this Stream stream, string value)
        {
            var buffer = Encoding.ASCII.GetBytes(value);

            stream.Write(buffer, 0, buffer.Length);
        }
    }
}