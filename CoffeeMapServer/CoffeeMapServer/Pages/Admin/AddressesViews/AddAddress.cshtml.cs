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
        public string Latitude { get; set; }

        [BindProperty]
        public string Longitude { get; set; }

        [BindProperty]
        public string RStatusCode { get; set; }

        public string Role { get; set; }

        public AddAddressModel(IAddressService addressService)
            => _addressService = addressService ?? throw new ArgumentNullException(nameof(IAddressService));

        public async Task<IActionResult> OnPostAsync()
        {
            var respCode = await _addressService.AddAddressAsync(Address, Latitude, Longitude);
            return respCode == 0 ? RedirectToPage("GetAddresses") :
                   respCode == -1 ? Redirect("601") : (IActionResult)StatusCode(400);
        }

        public void OnGetAsync(string statusCode)
        {
            RStatusCode = statusCode;
            Role = HttpContext.Request.Cookies[".AspNetCore.Meta.Metadata.role"].ToString();
        }
    }
}