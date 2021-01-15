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

        public string ErrorStatusCode { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var i = await _userService.AddUserAsync(Ruser);
            if (i == 0)
                return RedirectToPage("Users");
            else if (i == 601 || i == 602)
                return Redirect(i.ToString());
            else
                return BadRequest();
        }

        public void OnGet(string StatusCode)
        {
            ErrorStatusCode = StatusCode;
        }
    }
}