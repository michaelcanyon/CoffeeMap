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
        public GetAddressesModel(IAddessRepository repository)
        {
            addressRepository = repository;
        }

        [BindProperty]
        public int addressId { get; set; }
        public async Task OnGetAsync()
        {
            addresses = await addressRepository.GetList();
        }
    }
}
