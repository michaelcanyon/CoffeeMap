using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CoffeeMapServer.Infrastructures;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoffeeMapServer.Pages.Admin.UserViews
{
    [Authorize(Policy = Policies.Master)]
    public class AddUserModel : PageModel
    {
        private readonly IUserRepository _userRepository;
        public AddUserModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [BindProperty]
        public User user { get; set; }
        public int ErrorStatusCode { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            User userV = await _userRepository.GetSingle(user.Login);
            if (userV != null)
            {
                return RedirectToPage("AddUser");
            }
            userV = await _userRepository.GetSingleByMail(user.Email);
            if (userV != null)
            {
                return RedirectToPage("AddUser"); 
            
            }
            await _userRepository.Create(user);
            return RedirectToPage("Users");
        }
        public void OnGet(int StatusCode)
        {
            ErrorStatusCode = StatusCode;           
        }
    }
}
