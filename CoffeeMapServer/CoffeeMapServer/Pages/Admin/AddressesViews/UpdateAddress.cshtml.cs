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

        [BindProperty(SupportsGet =true)]
        public Address Address { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Role { get; set; }

        [BindProperty]
        public string Latitude { get; set; }

        [BindProperty]
        public string Longitude { get; set; }

        [BindProperty]
        public string RStatusCode { get; set; }

        public UpdateAddressModel(IAddressService addressService)
            => _addressService = addressService ?? throw new ArgumentNullException(nameof(IAddressService));

        public async Task<IActionResult> OnGetAsync(Guid id,
                                                    string statusCode)
        {
            RStatusCode = statusCode;
            Role = HttpContext.Request.Cookies[".AspNetCore.Meta.Metadata.role"].ToString();
            Address = await _addressService.GetSingleAddressByIdAsync(id);
            Latitude = Address.Latitude.ToString();
            Longitude = Address.Longitude.ToString();
            return Address == null ? BadRequest() : (IActionResult)Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var respCode=await _addressService.UpdateAddressAsync(Address,
                                                                  Latitude,
                                                                  Longitude);
            return respCode.Equals(0)
                ? RedirectToPage("GetAddresses")
                : respCode.Equals(-1) ? RedirectToPage("UpdateAddress",new { id = Address.Id, statusCode = "601" }) : (IActionResult)BadRequest();
        }
    }
}