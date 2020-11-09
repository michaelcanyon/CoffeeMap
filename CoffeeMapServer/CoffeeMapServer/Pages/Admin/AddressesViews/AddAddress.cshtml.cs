using System.Threading.Tasks;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using CoffeeMapServer.Services.Interfaces.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoffeeMapServer.Pages.Admin.AddressesViews
{
    public class AddAddressModel : PageModel
    {
        private readonly IAddressService _addressService;

        [BindProperty]
        public Address Address { get; set; }

        public string Role { get; set; }

        public AddAddressModel(IAddressService addressService)
            => _addressService = addressService;

        public void OnGet() 
            => Role = HttpContext.Request.Cookies[".AspNetCore.Meta.Metadata.role"].ToString();

        public async Task<IActionResult> OnPostAsync()
        {
            if (Address.OpeningHours == null)
                Address.OpeningHours = "none";

            await _addressService.AddAddressAsync(Address);
            return RedirectToPage("GetAddresses");

        }
    }
}
