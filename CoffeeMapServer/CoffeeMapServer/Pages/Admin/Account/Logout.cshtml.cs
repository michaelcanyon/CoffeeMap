using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoffeeMapServer.Pages.Admin.Account
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            try
            {
                Encryptions.QueryCookiesEditor.ClearCookies(HttpContext);
                return Redirect("~/Login");
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}