using System;
using System.Threading.Tasks;
using CoffeeMapServer.Infrastructures;
using CoffeeMapServer.Services.Interfaces.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoffeeMapServer.Pages.Admin.UserViews
{
    [Authorize(Policy = Policies.Master)]
    public class DeleteUserModel : PageModel
    {
        private readonly IUserService _userService;

        public DeleteUserModel(IUserService userService)
            => _userService = userService ?? throw new ArgumentNullException(nameof(IUserService));

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            await _userService.DeleteUserAsync(id);
            return RedirectToPage("Users");
        }
    }
}