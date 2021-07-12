using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CoffeeMapServer
{
    public static class AuthOptions
    {
        public const string ISSUER = "server"; // token producer

        public const string AUDIENCE = "user"; // token consumer

        const string KEY = "mysupersecret_secretkey!123";   // encryption key

        public const int LIFETIME = 30; // token lifetime

        public static SymmetricSecurityKey GetSymmetricSecurityKey() 
            => new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
    }
}