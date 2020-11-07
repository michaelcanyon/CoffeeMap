using System;
using System.Threading.Tasks;
using CoffeeMapServer.Services.Interfaces.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoffeeMapServer.Pages.Admin.RoasterViews
{
    public class DeleteRoasterModel : PageModel
    {
        private readonly IRoasterAdminService _roasterAdminService;

        public DeleteRoasterModel(IRoasterAdminService roasterAdminService)
            => _roasterAdminService = roasterAdminService;

        public async Task<IActionResult> OnGet(Guid id)
        {
            await _roasterAdminService.DeleteRoasterByIdAsync(id);
            return RedirectToPage("Roasters");
        }
    }
}
