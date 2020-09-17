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
    public class GetAddressesModel : PageModel
    {
        private readonly IAddessRepository addressRepository;
        public List<Address> addresses { get; set; }

        [BindProperty(SupportsGet = true)]
        public string addressIdFilter { get; set; }
        [BindProperty(SupportsGet = true)]
        public string addressStrFilter { get; set; }
        [BindProperty(SupportsGet = true)]
        public string openingHoursFilter { get; set; }
        public GetAddressesModel(IAddessRepository repository)
        {
            addressRepository = repository;
        }

        [BindProperty]
        public int addressId { get; set; }
        public string role { get; set; }
        public string nickname { get; set; }
        public async Task OnGetAsync()
        {
            nickname = HttpContext.Request.Cookies[".AspNetCore.Meta.Metadta.nickname"].ToString();
            role = HttpContext.Request.Cookies[".AspNetCore.Meta.Metadta.role"].ToString();
            addresses = await addressRepository.GetList();
            if (!string.IsNullOrEmpty(addressIdFilter))
                addresses = addresses.Where(n => n.Id.Equals(Convert.ToInt32(addressIdFilter))).ToList();
            if (!string.IsNullOrEmpty(addressStrFilter))
                addresses = addresses.Where(n => n.AddressStr.Contains(addressStrFilter)).ToList();
            if (!string.IsNullOrEmpty(openingHoursFilter))
                addresses = addresses.Where(n => n.OpeningHours.Contains(openingHoursFilter)).ToList();
        }
    }
}
