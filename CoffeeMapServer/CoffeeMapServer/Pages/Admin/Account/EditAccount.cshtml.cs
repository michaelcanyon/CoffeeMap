using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace CoffeeMapServer.Pages.Admin.Account
{
    public class EditAccountModel : PageModel
    {
        private readonly IUserRepository _userRepository;
        
        public EditAccountModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        [BindProperty]
        public User user { get; set; }
        
        public string PasswordHash { get; set; }
        
        [BindProperty]
        public string NewPassword { get; set; }
        
        public string Role { get; set; }
        
        public async Task OnGetAsync()
        {
            Role = HttpContext.Request.Cookies[".AspNetCore.Meta.Metadta.role"].ToString();
            var id = HttpContext.Request.Cookies[".AspNetCore.Meta.Metadta.id"].ToString();
            user = await _userRepository.GetSingle(Guid.Parse(id));
        }
        
        public async Task<IActionResult> OnPostAsync()
        {
            var getUser = await _userRepository.GetSingle(user.Id);
            if (CoffeeMapServer.Encryptions.Sha1Hash.GetHash(user.Password) != getUser.Password)
                return RedirectToPage("EditAccount");
            getUser.Password = NewPassword != null ? NewPassword : user.Password;
            getUser.Email = user.Email;
            await _userRepository.Update(getUser);
            return getUser.role == "Master" ? RedirectToPage("/Home/HomeMaster") : RedirectToPage("/Home/Home");
        }
    }
}
