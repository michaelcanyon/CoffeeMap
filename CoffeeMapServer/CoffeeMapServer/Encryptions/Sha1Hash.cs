using System.Security.Cryptography;
using System.Text;

namespace CoffeeMapServer.Encryptions
{
    public static class Sha1Hash
    {
        public static string GetHash(string password)
        {
            var sha1 = new System.Security.Cryptography.SHA1Managed();
            var plaintextBytes = Encoding.UTF8.GetBytes(password);
            var hashBytes = sha1.ComputeHash(plaintextBytes);

            var sb = new StringBuilder();
            foreach (var hashByte in hashBytes)
            {
                sb.AppendFormat("{0:x2}", hashByte);
            }
            var hashString = sb.ToString();
            return hashString;
        }
    }
}
