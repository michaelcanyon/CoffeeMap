using System;
using System.Threading.Tasks;
using CoffeeMapServer.Models;
using CoffeeMapServer.Services.Interfaces.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoffeeMapServer.Views.Admin.Addresses
{
    public class UpdateAddressModel : PageModel
    {
        private readonly IAddressService _addressService;

        [BindProperty]
        public Address Address { get; set; }

        public string Role { get; set; }

        public UpdateAddressModel(IAddressService addressService)
            => _addressService = addressService ?? throw new ArgumentNullException(nameof(IAddressService));

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            Role = HttpContext.Request.Cookies[".AspNetCore.Meta.Metadata.role"].ToString();
            Address = await _addressService.GetSingleAddressByIdAsync(id);
            if (Address.OpeningHours == "none")
                Address.OpeningHours = null;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Address.OpeningHours == null)
                Address.OpeningHours = "none";
            await _addressService.UpdateAddressAsync(Address);
            return RedirectToPage("GetAddresses");
        }
    }
}