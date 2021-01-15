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

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var respCode = await _roasterRequestService.DeleteRoasterRequestAsync(id);
            if (respCode.Equals(0))
                return RedirectToPage("RoasterRequests");
            else
                return BadRequest();
        }
    }
}