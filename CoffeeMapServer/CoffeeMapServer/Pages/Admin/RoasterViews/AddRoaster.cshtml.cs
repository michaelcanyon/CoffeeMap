using System.Threading.Tasks;
using CoffeeMapServer.Models;
using CoffeeMapServer.Services.Interfaces.Admin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoffeeMapServer.Views.Admin.RoasterViews
{
    public class AddRoasterModel : PageModel
    {
        private readonly IRoasterAdminService _roasterAdminService;

        [BindProperty]
        public Roaster Roaster { get; set; }

        [BindProperty]
        public IFormFile Picture { get; set; }

        [BindProperty]
        public Address Address { get; set; }

        [BindProperty]
        public string Tags { get; set; }

        public string Role { get; set; }

        public AddRoasterModel(IRoasterAdminService roasterAdminService)
            => _roasterAdminService = roasterAdminService;

        public void OnGet()
           => Role = HttpContext.Request.Cookies[".AspNetCore.Meta.Metadata.role"].ToString();

        public async Task<IActionResult> OnPostAsync()
        {
            await _roasterAdminService.AddRoasterAsync(Roaster, Tags, Address, Picture);
            return RedirectToPage("Roasters");

        }
    }
}
