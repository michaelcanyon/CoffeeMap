using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Infrastructures.Repositories;
using CoffeeMapServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoffeeMapServer.Pages.Admin.AddressesViews
{
    public class AddAddressModel : PageModel
    {
        private readonly IAddessRepository addessRepository;

        [BindProperty]
        public Address address { get; set; }
        public AddAddressModel(IAddessRepository addrRepository)
        {
            addessRepository = addrRepository;
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (address.OpeningHours == null)
                address.OpeningHours = "none";

            await addessRepository.Create(address);
            return RedirectToPage("GetAddresses");

        }
    }
}
