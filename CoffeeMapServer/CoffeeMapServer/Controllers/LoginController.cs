using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CoffeeMapServer.EF;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using CoffeeMapServer.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CoffeeMapServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private readonly IConfiguration config;
        private readonly IUserRepository userRepository;
        public LoginController(IUserRepository repository, IConfiguration configuration)
        {
            userRepository = repository;
            config = configuration;
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            HttpContext.Response.Cookies.Append(".AspNetCore.Meta.Metadta", "");
            HttpContext.Response.Cookies.Append(".AspNetCore.Meta.Metadta.id", "");
            HttpContext.Response.Cookies.Append(".AspNetCore.Meta.Metadta.nickname", "");
            HttpContext.Response.Cookies.Append(".AspNetCore.Meta.Metadta.role", "");
            HttpContext.Response.Cookies.Append(".AspNetCore.Meta.Metadta.hash", "");
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromForm]LoginModel login)
        {
            IActionResult responce = Unauthorized();
            var token =await Token(login.Email, login.Password);
            if (token == null)
                return View();
            var userSample =await userRepository.GetSingle(login.Email, login.Password);
            HttpContext.Response.Cookies.Append(".AspNetCore.Meta.Metadta", token);
            HttpContext.Response.Cookies.Append(".AspNetCore.Meta.Metadta.id", userSample.Id.ToString());
            HttpContext.Response.Cookies.Append(".AspNetCore.Meta.Metadta.nickname", userSample.Login);
            HttpContext.Response.Cookies.Append(".AspNetCore.Meta.Metadta.role", userSample.role);
            HttpContext.Response.Cookies.Append(".AspNetCore.Meta.Metadta.hash", userSample.Password);
            return userSample.role == "Master" ? Redirect("~/Home/HomeMaster") : Redirect("~/Home/Home");
        }
        //[Authorize(Roles = "admin")]
        //[Route("getrole")]
        //public IActionResult GetRole()
        //{
        //    return Ok("Ваша роль: администратор");
        //}
        // [HttpPost("/token")]
        private async Task<string> Token(string username, string password)
        {
            var identity = await GetIdentity(username, password);
            if(identity==null)
                return null;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: DateTime.UtcNow,
                    claims: identity.Claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return encodedJwt;
        }
        private async Task<ClaimsIdentity> GetIdentity(string username, string password)
        {
            var user =await userRepository.GetSingle(username, password);
            if (user == null)
                return null;
            //the last one claim ???
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim("role", user.role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

            };
            ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token");
            return claimsIdentity;
        }
    }
}
