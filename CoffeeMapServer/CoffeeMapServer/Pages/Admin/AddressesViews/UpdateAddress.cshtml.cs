using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace CoffeeMapServer.Views.Admin.Addresses
{
    public class UpdateAddressModel : PageModel
    {
        private readonly IAddessRepository addressRepository;

        [BindProperty]
        public Address Address { get; set; }

        public string Role { get; set; }

        public string Nickname { get; set; }

        public UpdateAddressModel(IAddessRepository repository)
        {
            addressRepository = repository;
        }

        public async Task<IActionResult> OnGet(Guid id)
        {
            Nickname = HttpContext.Request.Cookies[".AspNetCore.Meta.Metadta.nickname"].ToString();
            Role = HttpContext.Request.Cookies[".AspNetCore.Meta.Metadta.role"].ToString();
            Address = await addressRepository.GetSingle(id);
            if (Address.OpeningHours == "none")
                Address.OpeningHours = null;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Address.OpeningHours == null)
                Address.OpeningHours = "none";
            await addressRepository.Update(Address);
            return RedirectToPage("GetAddresses");
        }
    }
}
