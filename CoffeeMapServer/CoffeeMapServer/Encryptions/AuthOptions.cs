using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CoffeeMapServer
{
    public static class AuthOptions
    {
        public const string ISSUER = "http://localhost:5000/"; // token producer

        public const string AUDIENCE = "http://localhost:5000/"; // token consumer

        const string KEY = "mysupersecret_secretkey!123";   // encryption key

        public const int LIFETIME = 30; // token lifetime

        public static SymmetricSecurityKey GetSymmetricSecurityKey() 
            => new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
    }
}