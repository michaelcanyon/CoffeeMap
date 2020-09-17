using System.Security.Cryptography;
using System.Text;

namespace CoffeeMapServer.Encryptions
{
    public static class Sha1Hash
    {
        public static string GetHash(string password)
        {
            var sha = new SHA1CryptoServiceProvider();
            var passwordByte = Encoding.UTF8.GetBytes(password);
            var hash = sha.ComputeHash(passwordByte);
            return System.BitConverter.ToString(hash);
        }
    }
}
