using CoffeeMapServer.Infrastructures;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Services.Interfaces.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace CoffeeMapServer.Pages.Admin.UserViews
{
    [Authorize(Policy = Policies.Master)]
    public class DeleteUserModel : PageModel
    {
        private readonly IUserService _userService;

        public Guid Guid { get; set; }

        public DeleteUserModel(IUserService userService) => _userService= userService;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            Guid = id;
            await _userService.DeleteUserAsync(Guid);
            return RedirectToPage("Users");
        }
    }
}
