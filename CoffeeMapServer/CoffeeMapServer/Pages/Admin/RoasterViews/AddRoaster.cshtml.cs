using System;
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

        [BindProperty]
        public string RStatusCode { get; set; }

        public AddRoasterModel(IRoasterAdminService roasterAdminService)
            => _roasterAdminService = roasterAdminService ?? throw new ArgumentNullException(nameof(IRoasterAdminService));

        public IActionResult OnGet(string StatusCode)
        {
            try
            {
                Role = HttpContext.Request.Cookies[".AspNetCore.Meta.Metadata.role"].ToString();
                RStatusCode = StatusCode;
                return Page();
            }
            catch
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var respCode = await _roasterAdminService.AddRoasterAsync(Roaster, Tags, Address, Picture);
            if (respCode == 0)
                return RedirectToPage("Roasters");
            else if (respCode == -1)
                return Redirect("601");
            else
                return BadRequest();
        }
    }
}