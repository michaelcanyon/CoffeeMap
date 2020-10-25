using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Infrastructures.Repositories;
using CoffeeMapServer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CoffeeMapServer.Services
{
    public class IdentityGeneratorService:IIdentityGeneratorService
    {
        private readonly IUserRepository _userRepository;
        public IdentityGeneratorService(IUserRepository repository)
        {
            _userRepository = repository;
        }

        public async Task<ClaimsIdentity> GetIdentity(string username, string password)
        {
            var user = await _userRepository.GetSingle(username, password);
            if (user == null)
                return null;
            //the last one claim ???
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim("role", user.role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            return new ClaimsIdentity(claims, "Token");
        }
    }
}
