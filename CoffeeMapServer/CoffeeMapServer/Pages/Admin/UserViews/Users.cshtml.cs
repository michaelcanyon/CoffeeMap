using System;
using System.Collections.Generic;
using System.Linq;
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
    public class UsersModel : PageModel
    {
        private readonly IUserService _userService;

        [BindProperty(SupportsGet = true)]
        public string IdFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string LoginFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string EmailFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string RoleFilter { get; set; }

        public IList<User> Users { get; set; }

        public UsersModel(IUserService userService)
            => _userService = userService ?? throw new ArgumentNullException(nameof(IUserService));

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                Users = await _userService.FetchUsersAsync();
                if (!string.IsNullOrEmpty(IdFilter))
                    Users = Users
                            .Where(user => user.Id.ToString().Equals(Convert.ToString(IdFilter)))
                            .ToList();
                if (!string.IsNullOrEmpty(LoginFilter))
                    Users = Users
                            .Where(user => user.Login.Contains(LoginFilter))
                            .ToList();
                if (!string.IsNullOrEmpty(EmailFilter))
                    Users = Users
                            .Where(user => user.Email.Contains(EmailFilter))
                            .ToList();
                if (!string.IsNullOrEmpty(RoleFilter))
                    Users = Users
                            .Where(user => user.Role.Equals(RoleFilter))
                            .ToList();
                return Page();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}