using System;
using System.Threading.Tasks;
using CoffeeMapServer.Services.Interfaces.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoffeeMapServer.Views.Admin.Addresses
{
    public class DeleteAddressModel : PageModel
    {
        private readonly IAddressService _addressService;

        public DeleteAddressModel(IAddressService addressService)
            => _addressService = addressService ?? throw new ArgumentNullException(nameof(IAddressService));

        public async Task<IActionResult> OnGet(Guid id)
        {
           var respCode= await _addressService.DeleteAddressAsync(id);
            if (respCode.Equals(0))
                return RedirectToPage("GetAddresses");
            else
                return BadRequest();
        }
    }
}