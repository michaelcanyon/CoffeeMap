using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;

namespace CoffeeMapServer.Pages.Admin.UserViews
{
    public class UsersModel : PageModel
    {
        private readonly IUserRepository _userRepository;
        public UsersModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        
        }
        public List<User> users { get; set; }
        public async Task OnGet()
        {
            users = await _userRepository.GetList();
        }
    }
}
