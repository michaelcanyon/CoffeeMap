using System;
using System.Threading.Tasks;
using CoffeeMapServer.Services.Interfaces.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoffeeMapServer.Pages.Admin.RoasterRequestViews
{
    public class BindToRoasterModel : PageModel
    {
        private readonly IRoasterRequestService _roasterRequestService;

        public BindToRoasterModel(IRoasterRequestService roasterRequestService)
            => _roasterRequestService = roasterRequestService ?? throw new ArgumentNullException(nameof(IRoasterRequestService));

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            await _roasterRequestService.BindToRoasterNdAddressAsync(id);
            return RedirectToPage("RoasterRequests");
        }
    }
}