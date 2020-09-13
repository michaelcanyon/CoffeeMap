using System.Security.Cryptography;
using System.Text;

namespace CoffeeMapServer.Encryptions
{
    public static class Sha1Hash
    {
        public static string GetHash(string password)
        {
            var sha = new SHA1CryptoServiceProvider();
            var passwordByte = Encoding.ASCII.GetBytes(password);
            var hash = sha.ComputeHash(passwordByte);
            return ASCIIEncoding.ASCII.GetString(hash);
        }
    }
}
