using CoffeeMapServer.Infrastructures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoffeeMapServer.Pages.Admin.Home
{
    [Authorize(Policy =Policies.Admin)]
    public class HomeModel : PageModel
    {
        public void OnGet()
        { }
    }
}
