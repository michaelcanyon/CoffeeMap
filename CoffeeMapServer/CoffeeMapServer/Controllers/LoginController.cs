using CoffeeMapServer.Encryptions;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Services.Interfaces;
using CoffeeMapServer.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CoffeeMapServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly IIdentityGeneratorService _identityGeneratorService;

        public LoginController(IUserRepository repository, IIdentityGeneratorService identityGeneratorService)
        {
            userRepository = repository;
            _identityGeneratorService = identityGeneratorService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            await QueryCookiesEditor.ClearCookies(HttpContext);
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromForm] LoginViewModel login)
        {
            var identity =await _identityGeneratorService.GetIdentity(login.Email, login.Password);
            if (identity == null)
                return View();
            var token = await TokenGenerator.GenerateToken(identity);
            var userSample = await userRepository.GetSingle(login.Email, login.Password);
            await QueryCookiesEditor.SetUserCookies(userSample, token, HttpContext);
            return userSample.role == "Master" ? Redirect("~/Home/HomeMaster") : Redirect("~/Home/Home");
        }

    }
}
