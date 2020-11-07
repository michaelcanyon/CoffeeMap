﻿using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Services.Interfaces;
using CoffeeMapServer.Services.Interfaces.Admin;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CoffeeMapServer.Services
{
    public class IdentityGeneratorService : IIdentityGeneratorService
    {
        private readonly IUserService _userService;

        public IdentityGeneratorService(IUserService repository) => _userService = repository;

        public async Task<ClaimsIdentity> GetIdentity(string username, string password)
        {
            var user = await _userService.Login(username, password);
            if (user == null)
                return null;
            //the last one claim ???
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
