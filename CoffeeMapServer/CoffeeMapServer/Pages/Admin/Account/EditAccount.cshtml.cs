using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
        public string passwordHash { get; set; }
        [BindProperty]
        public string newPassword { get; set; }
        public string role { get; set; }
        public string hash { get; set; }
        public async Task OnGetAsync()
        {
            role = HttpContext.Request.Cookies[".AspNetCore.Meta.Metadta.role"].ToString();
            hash = HttpContext.Request.Cookies[".AspNetCore.Meta.Metadta.hash"].ToString();
            var id = Convert.ToInt32(HttpContext.Request.Cookies[".AspNetCore.Meta.Metadta.id"].ToString());
            user = await _userRepository.GetSingle(id);
            if (user.Password != passwordHash)
                 RedirectToPage("Home");
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var getUser= await _userRepository.GetSingle(user.Id);
            var hashss = CoffeeMapServer.Encryptions.Sha1Hash.GetHash(user.Password);
            if (CoffeeMapServer.Encryptions.Sha1Hash.GetHash(user.Password) != getUser.Password)
                    return RedirectToPage("EditAccount");
            getUser.Password= newPassword != null ? newPassword : user.Password;
            getUser.Email=user.Email;
           await _userRepository.Update(getUser);
            return getUser.role=="Master" ? RedirectToPage("/Home/HomeMaster") : RedirectToPage("/Home/Home");
        }
    }
}
