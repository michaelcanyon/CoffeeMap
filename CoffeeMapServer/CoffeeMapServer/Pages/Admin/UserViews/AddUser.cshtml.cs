using System;
using System.Threading.Tasks;
using CoffeeMapServer.Infrastructures;
using CoffeeMapServer.Models;
using CoffeeMapServer.Services.Interfaces.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoffeeMapServer.Pages.Admin.UserViews
{
    [Authorize(Policy = Policies.Master)]
    public class AddUserModel : PageModel
    {
        private readonly IUserService _userService;

        public AddUserModel(IUserService userService)
            => _userService = userService ?? throw new ArgumentNullException(nameof(IUserService));

        [BindProperty]
        public User Ruser { get; set; }

        public int ErrorStatusCode { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            await _userService.AddUserAsync(Ruser);
            return RedirectToPage("Users");
        }

        public void OnGet(int StatusCode)
            => ErrorStatusCode = StatusCode;
    }
}