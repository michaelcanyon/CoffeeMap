using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace CoffeeMapServer.Pages.Admin.AddressesViews
{
    public class AddAddressModel : PageModel
    {
        private readonly IAddessRepository addessRepository;

        [BindProperty]
        public Address Address { get; set; }

        public string Role { get; set; }

        public string Nickname { get; set; }

        public AddAddressModel(IAddessRepository addrRepository)
        {
            addessRepository = addrRepository;
        }

        public async Task OnGetAsync()
        {
            Nickname = HttpContext.Request.Cookies[".AspNetCore.Meta.Metadta.nickname"].ToString();
            Role = HttpContext.Request.Cookies[".AspNetCore.Meta.Metadta.role"].ToString();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Address.OpeningHours == null)
                Address.OpeningHours = "none";

            await addessRepository.Create(Address);
            return RedirectToPage("GetAddresses");

        }
    }
}
