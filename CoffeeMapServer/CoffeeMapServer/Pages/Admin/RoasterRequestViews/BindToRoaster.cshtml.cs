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

        // TODO: если не будет работать, верни поле состояния guid
        public BindToRoasterModel(IRoasterRequestService roasterRequestService)
            => _roasterRequestService = roasterRequestService;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            await _roasterRequestService.BindToRoasterNdAddressAsync(id);
            return RedirectToPage("RoasterRequests");
        }
    }
}
