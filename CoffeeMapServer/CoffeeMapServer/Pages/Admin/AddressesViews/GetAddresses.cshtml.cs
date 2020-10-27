using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeMapServer.Views.Admin.Addresses
{
    public class GetAddressesModel : PageModel
    {
        private readonly IAddessRepository addressRepository;

        public List<Address> Addresses { get; set; }

        public GetAddressesModel(IAddessRepository repository)
        {
            addressRepository = repository;
        }

        [BindProperty(SupportsGet = true)]
        public string AddressIdFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string AddressStrFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string OpeningHoursFilter { get; set; }

        [BindProperty]
        public int AddressId { get; set; }

        public string Role { get; set; }

        public string Nickname { get; set; }

        public async Task OnGetAsync()
        {
            Nickname = HttpContext.Request.Cookies[".AspNetCore.Meta.Metadta.nickname"].ToString();
            Role = HttpContext.Request.Cookies[".AspNetCore.Meta.Metadta.role"].ToString();
            Addresses = await addressRepository.GetList();
            if (!string.IsNullOrEmpty(AddressIdFilter))
                Addresses = Addresses.Where(n => n.Id.Equals(AddressIdFilter)).ToList();
            if (!string.IsNullOrEmpty(AddressStrFilter))
                Addresses = Addresses.Where(n => n.AddressStr.Contains(AddressStrFilter)).ToList();
            if (!string.IsNullOrEmpty(OpeningHoursFilter))
                Addresses = Addresses.Where(n => n.OpeningHours.Contains(OpeningHoursFilter)).ToList();
        }
    }
}
