using System;
using System.Threading.Tasks;
using CoffeeMapServer.Services.Interfaces.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoffeeMapServer.Pages.Admin.RoasterRequestViews
{
    public class DeleteRoasterRequestModel : PageModel
    {
        private readonly IRoasterRequestService _roasterRequestService;

        public DeleteRoasterRequestModel(IRoasterRequestService roasterRequestService)
            => _roasterRequestService = roasterRequestService ?? throw new ArgumentNullException(nameof(IRoasterRequestService));

        public async Task<IActionResult> OnGetAsync(Guid guid)
        {
            await _roasterRequestService.DeleteRoasterRequestAsync(guid);
            return RedirectToPage("RoasterRequests");
        }
    }
}