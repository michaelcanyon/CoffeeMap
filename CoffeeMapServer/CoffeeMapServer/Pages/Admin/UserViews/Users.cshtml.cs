using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using Microsoft.AspNetCore.Authorization;
using CoffeeMapServer.Infrastructures;

namespace CoffeeMapServer.Pages.Admin.UserViews
{
    [Authorize(Policy = Policies.Master)]
    public class UsersModel : PageModel
    {
        private readonly IUserRepository _userRepository;
        [BindProperty]
        public string idFilter { get; set; }
        [BindProperty]
        public string loginFilter { get; set; }
        [BindProperty]
        public string emailFilter { get; set; }
        [BindProperty]
        public string roleFilter { get; set; }
        public List<User> users { get; set; }
        public UsersModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        
        }
        public async Task OnGetAsync()
        {
            users = await _userRepository.GetList();
            if (!string.IsNullOrEmpty(idFilter))
                users = users.Where(user => user.Id.Equals(Convert.ToInt32(idFilter))).ToList();
            if (!string.IsNullOrEmpty(loginFilter))
                users = users.Where(user => user.Login.Contains(loginFilter)).ToList();
            if (!string.IsNullOrEmpty(emailFilter))
                users = users.Where(user => user.Email.Contains(emailFilter)).ToList();
            if (!string.IsNullOrEmpty(roleFilter))
                users = users.Where(user => user.role.Equals(roleFilter)).ToList();

        }
    }
}
