using System.Security.Cryptography;
using System.Text;

namespace OamCake.Common.Helpers
{
    public static class Cypher
    {
        public static string HashedPassword(this string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return String.Empty;
            }

            byte[] buffer = Encoding.UTF8.GetBytes(password);
            byte[] digest = SHA256.HashData(buffer);
            return BitConverter.ToString(digest, 0, buffer.Length);
        }
    }
}
