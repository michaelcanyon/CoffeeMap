using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeMapServer.Models;
using CoffeeMapServer.Services.Interfaces.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoffeeMapServer.Pages.Admin.RoasterRequestViews
{
    public class RoasterRequestsModel : PageModel
    {
        private readonly IRoasterRequestService _roasterRequestService;

        public RoasterRequestsModel(IRoasterRequestService roasterRequestService)
            => _roasterRequestService = roasterRequestService ?? throw new ArgumentNullException(nameof(IRoasterRequestService));

        [BindProperty]
        public IList<RoasterRequest> RoasterRequests { get; set; }

        public string Role { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Role = HttpContext.Request.Cookies[".AspNetCore.Meta.Metadata.role"].ToString();
            RoasterRequests = await _roasterRequestService.FetchRoasterRequestsListAsync();

            return RoasterRequests == null ? BadRequest() : (IActionResult)Page();
        }

        public async Task<IActionResult> OnPostDeleteAllAsync()
        {
            var respCode = await _roasterRequestService.DeleteAllRoasterRequestsAsync();
            return respCode.Equals(0) ? RedirectToPage("RoasterRequests") : (IActionResult)BadRequest();
        }
    }
}