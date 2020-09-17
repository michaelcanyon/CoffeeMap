using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeMapServer.Infrastructures;
using CoffeeMapServer.Infrastructures.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoffeeMapServer.Pages.Admin.UserViews
{
    [Authorize(Policy = Policies.Master)]
    public class DeleteUserModel : PageModel
    {
        private readonly IUserRepository _userRepository;
        public DeleteUserModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            await _userRepository.Delete(id);
            return RedirectToPage("Users");
        }
    }
}
