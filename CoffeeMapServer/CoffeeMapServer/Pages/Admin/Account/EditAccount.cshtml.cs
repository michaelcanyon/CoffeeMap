using System;
using System.Threading.Tasks;
using CoffeeMapServer.Models;
using CoffeeMapServer.Services.Interfaces.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoffeeMapServer.Pages.Admin.Account
{
    public class EditAccountModel : PageModel
    {
        private readonly IAccountService _accountService;

        public EditAccountModel(IAccountService accountService)
            => _accountService = accountService ?? throw new ArgumentNullException(nameof(IAccountService));

        [BindProperty]
        public User _User { get; set; }

        [BindProperty]
        public string NewPassword { get; set; }

        public string Role { get; set; }

        [BindProperty]
        public string RStatusCode { get; set; }

        public async Task<IActionResult> OnGetAsync(string statusCode)
        {

            RStatusCode = statusCode;
            Role = HttpContext.Request.Cookies[".AspNetCore.Meta.Metadata.role"].ToString();
            var id = HttpContext.Request.Cookies[".AspNetCore.Meta.Metadata.id"].ToString();
            _User = await _accountService.GetAccountByIdAsync(Guid.Parse(id));
            if (_User == null)
                return BadRequest();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var getUser = await _accountService.GetAccountByIdAsync(_User.Id);
            if(_User.Password!=null)
                if (Encryptions.Sha1Hash.GetHash(_User.Password) != getUser.Password)
                    return Redirect("602");
            var passwordHash = Encryptions.Sha1Hash.GetHash(NewPassword);
            var respCode = await _accountService.UpdateAccountAsync(getUser,
                                                                    passwordHash,
                                                                    _User.Email);
            if (respCode.Equals(0))
                return getUser.Role == "Master" ? RedirectToPage("/Home/HomeMaster") : RedirectToPage("/Home/Home");
            else if (respCode.Equals(-1))
                return Redirect("601");
            else
                return BadRequest();
        }
    }
}