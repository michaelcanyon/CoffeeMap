using System;
using System.Threading.Tasks;
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

        [BindProperty]
        public string RStatusCode { get; set; }

        public string Role { get; set; }

        public AddAddressModel(IAddressService addressService)
            => _addressService = addressService ?? throw new ArgumentNullException(nameof(IAddressService));

        public void OnGet()
            => Role = HttpContext.Request.Cookies[".AspNetCore.Meta.Metadata.role"].ToString();

        public async Task<IActionResult> OnPostAsync()
        {
           var respCode= await _addressService.AddAddressAsync(Address);
            if (respCode == 0)
                return RedirectToPage("GetAddresses");
            else if (respCode == -1)
                return Redirect("601");
            else 
                return StatusCode(400);
        }

        public void OnGetAsync(string StatusCode)
            => RStatusCode = StatusCode;
    }
}