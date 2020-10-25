using CoffeeMapServer.Infrastructures;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CoffeeMapServer.Services
{
    /// <summary>
    /// implements jwt generation service initialization
    /// </summary>
    public static class AuthService
    {
        public static void ConfigureAuth(this IServiceCollection collection)
        {
            collection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // enables token producer validation
                        ValidateIssuer = true,
                        // producer information variable
                        ValidIssuer = AuthOptions.ISSUER,

                        // enables consumer validation
                        ValidateAudience = true,
                        // consumer information variable
                        ValidAudience = AuthOptions.AUDIENCE,
                        // token existance time validation
                        ValidateLifetime = true,

                        // security key setting
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                        // security key validation
                        ValidateIssuerSigningKey = true,
                    };
                });

            //Add policies to token authorization
            collection.AddAuthorization(config =>
            {
                config.AddPolicy(Policies.Admin, Policies.AdminPolicy());
                config.AddPolicy(Policies.Master, Policies.MasterPolicy());
            });
        }
    }
}
