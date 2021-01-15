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
            => _roasterAdminService = roasterAdminService ?? throw new ArgumentNullException(nameof(IRoasterAdminService));

        public async Task<IActionResult> OnGet(Guid id)
        {
           var respCode= await _roasterAdminService.DeleteRoasterByIdAsync(id);
            if (respCode.Equals(0))
                return RedirectToPage("Roasters");
            else
                return BadRequest();
        }
    }
}