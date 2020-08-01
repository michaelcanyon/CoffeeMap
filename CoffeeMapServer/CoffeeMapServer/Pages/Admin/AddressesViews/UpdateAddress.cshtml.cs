using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoffeeMapServer.Views.Admin.Addresses
{
    public class UpdateAddressModel : PageModel
    {
        private readonly IAddessRepository addressRepository;

        [BindProperty]
        public Address address { get; set; }
        public UpdateAddressModel(IAddessRepository repository)
        {
            addressRepository = repository;
        }
        public async Task<IActionResult> OnGet(int? id)
        {
            address = await addressRepository.GetSingle(Convert.ToInt32(id));
            if (address.OpeningHours == "none")
                address.OpeningHours = null;
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (address.OpeningHours == null)
                address.OpeningHours = "none";
            await addressRepository.Update(address);
            return RedirectToPage("GetAddresses");
        }
    }
}
