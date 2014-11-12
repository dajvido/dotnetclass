using System;
using System.Text;

namespace Common
{
    public static class Extensions
    {
        public static byte[] GetBytes(this string message)
        {
            if (message == null) throw new ArgumentNullException("message");
            return Encoding.UTF8.GetBytes(message);
        }

        public static string GetString(this byte[] message)
        {
            if (message == null) throw new ArgumentNullException("message");

            return Encoding.UTF8.GetString(message);
        }
    }
}