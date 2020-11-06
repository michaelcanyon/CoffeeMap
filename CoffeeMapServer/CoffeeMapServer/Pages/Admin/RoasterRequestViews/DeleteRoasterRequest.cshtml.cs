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

        public Guid guid { get; set; }

        public DeleteRoasterRequestModel(IRoasterRequestService roasterRequestService)
            => _roasterRequestService = roasterRequestService;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            guid = id;
            await _roasterRequestService.DeleteRoasterRequestAsync(guid);
            return RedirectToPage("RoasterRequests");
        }
    }
}
