using System;
using System.Threading.Tasks;
using CoffeeMapServer.Encryptions;
using CoffeeMapServer.Services.Interfaces;
using CoffeeMapServer.Services.Interfaces.Admin;
using CoffeeMapServer.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeMapServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private readonly IUserService userService;
        private readonly IIdentityGeneratorService _identityGeneratorService;

        public LoginController(IUserService repository, IIdentityGeneratorService identityGeneratorService)
        {
            userService = repository ?? throw new ArgumentNullException(nameof(IUserService));
            _identityGeneratorService = identityGeneratorService ?? throw new ArgumentNullException(nameof(IIdentityGeneratorService));
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            QueryCookiesEditor.ClearCookies(HttpContext);
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromForm] LoginViewModel login)
        {
            var identity = await _identityGeneratorService.GetIdentity(login.Email, login.Password);
            if (identity == null)
                return View();
            var token = await TokenGenerator.GenerateToken(identity);
            var userSample = await userService.Login(login.Email, login.Password);
            QueryCookiesEditor.SetUserCookies(userSample, token, HttpContext);
            return userSample.Role == "Master" ? Redirect("~/Home/HomeMaster") : Redirect("~/Home/Home");
        }
    }
}