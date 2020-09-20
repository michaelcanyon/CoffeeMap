using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoffeeMapServer.Pages.Admin.Account
{
    public class LogoutModel : PageModel
    {
        public async Task<IActionResult> OnGetAsync()
        {
            HttpContext.Response.Cookies.Append(".AspNetCore.Meta.Metadta", "");
            HttpContext.Response.Cookies.Append(".AspNetCore.Meta.Metadta.id", "");
            HttpContext.Response.Cookies.Append(".AspNetCore.Meta.Metadta.nickname", "");
            HttpContext.Response.Cookies.Append(".AspNetCore.Meta.Metadta.role", "");
            HttpContext.Response.Cookies.Append(".AspNetCore.Meta.Metadta.hash", "");
            return Redirect("~/Login");
        }
    }
}
