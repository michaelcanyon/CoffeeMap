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

        public Guid Guid { get; set; }

        public DeleteAddressModel(IAddressService addressService)
            => _addressService = addressService ?? throw new ArgumentNullException(nameof(IAddressService));

        public async Task<IActionResult> OnGet(Guid id)
        {
            Guid = id;
            await _addressService.DeleteAddressAsync(id);
            return RedirectToPage("GetAddresses");
        }
    }
}