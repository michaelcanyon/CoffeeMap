using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeMapServer.Models;
using CoffeeMapServer.Services.Interfaces.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoffeeMapServer.Views.Admin.Addresses
{
    public class GetAddressesModel : PageModel
    {
        private readonly IAddressService _addressService;

        public IList<Address> Addresses { get; set; }

        public GetAddressesModel(IAddressService addressService)
            => _addressService = addressService;

        [BindProperty(SupportsGet = true)]
        public string AddressIdFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string AddressStrFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string OpeningHoursFilter { get; set; }

        public string Role { get; set; }

        public async Task OnGetAsync()
        {
            Role = HttpContext.Request.Cookies[".AspNetCore.Meta.Metadata.role"].ToString();
            Addresses = await _addressService.FetchAddressesAsync();
            if (!string.IsNullOrEmpty(AddressIdFilter))
                Addresses = Addresses.Where(n => n.Id.Equals(AddressIdFilter)).ToList();
            if (!string.IsNullOrEmpty(AddressStrFilter))
                Addresses = Addresses.Where(n => n.AddressStr.Contains(AddressStrFilter)).ToList();
            if (!string.IsNullOrEmpty(OpeningHoursFilter))
                Addresses = Addresses.Where(n => n.OpeningHours.Contains(OpeningHoursFilter)).ToList();
        }
    }
}
