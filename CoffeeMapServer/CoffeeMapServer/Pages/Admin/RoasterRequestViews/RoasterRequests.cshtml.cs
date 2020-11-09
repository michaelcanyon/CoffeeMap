using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using CoffeeMapServer.Services.Interfaces.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoffeeMapServer.Pages.Admin.RoasterRequestViews
{
    public class RoasterRequestsModel : PageModel
    {
        private readonly IRoasterRequestService _roasterRequestService;

        public RoasterRequestsModel(IRoasterRequestService roasterRequestService)
            =>_roasterRequestService = roasterRequestService;

        [BindProperty]
        public IList<RoasterRequest> RoasterRequests { get; set; }

        public string Role { get; set; }

        public async Task OnGetAsync()
        {
            Role = HttpContext.Request.Cookies[".AspNetCore.Meta.Metadata.role"].ToString();
            RoasterRequests = await _roasterRequestService.FetchRoasterRequestsListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAllAsync()
        {
            await _roasterRequestService.DeleteAllRoasterRequestsAsync();
            return RedirectToPage("RoasterRequests");
        }
    }
}
