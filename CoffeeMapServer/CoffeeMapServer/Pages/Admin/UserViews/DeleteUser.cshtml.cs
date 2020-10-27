using CoffeeMapServer.Infrastructures;
using CoffeeMapServer.Infrastructures.IRepositories;
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
        private readonly IUserRepository _userRepository;

        public Guid Guid { get; set; }

        public DeleteUserModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            Guid = id;
            await _userRepository.Delete(Guid);
            return RedirectToPage("Users");
        }
    }
}
