using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeMapServer.Infrastructures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoffeeMapServer.Pages.Home
{
    [Authorize(Policy = Policies.Master)]
    public class HomeMasterModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
