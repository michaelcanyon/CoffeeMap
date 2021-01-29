using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using CoffeeMapServer.Services.Interfaces;
using CoffeeMapServer.Services.Interfaces.Admin;

namespace CoffeeMapServer.Services
{
    public class IdentityGeneratorService : IIdentityGeneratorService
    {
        private readonly IUserService _userService;

        public IdentityGeneratorService(IUserService userService)
            => _userService = userService ?? throw new ArgumentNullException(nameof(userService));

        public async Task<ClaimsIdentity> GetIdentity(string username, string password)
        {
            var user = await _userService.Login(username, password);
            if (user == null)
                return null;

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim("role", user.Role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            return new ClaimsIdentity(claims, "Token");
        }
    }
}