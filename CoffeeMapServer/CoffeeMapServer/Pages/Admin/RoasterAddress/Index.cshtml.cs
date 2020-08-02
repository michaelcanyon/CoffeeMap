using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Infrastructures.Repositories;
using CoffeeMapServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoffeeMapServer.Pages.Admin.RoasterAddress
{
    public class IndexModel : PageModel
    {
        private readonly IRoasterRepository roasterRepository;
        private readonly IAddessRepository addessRepository;
        public IndexModel(IRoasterRepository rRepository, IAddessRepository aRepository)
        {
            roasterRepository = rRepository;
            addessRepository = aRepository;
        }
        public string idFilter { get; set; }
        public string nameFilter { get; set; }
        public string addressIdFilter { get; set; }
        public string addressStrFilter { get; set; }
        public List<Address> addresses { get; set; }
        public List<Roaster> roasters { get; set; }

        public Roaster insertableRoaster { get; set; }
        public Address insertableAddress { get; set; }

        public string insertRoasterId { get; set; }
        public string insertAddressId { get; set; }
        public async Task OnGetAsync()
        {
            addresses = await addessRepository.GetList();
            roasters = await roasterRepository.GetList();
            if (!string.IsNullOrEmpty(addressIdFilter))
                addresses = addresses.Where(n => n.Id.Equals(Convert.ToInt32(addressIdFilter))).ToList();
            if (!string.IsNullOrEmpty(addressStrFilter))
                addresses = addresses.Where(n => n.AddressStr.Contains(addressStrFilter)).ToList();
            if (!string.IsNullOrEmpty(idFilter))
                roasters = roasters.Where(n => n.Id.Equals(Convert.ToInt32(idFilter))).ToList();
            if (!string.IsNullOrEmpty(nameFilter))
                roasters = roasters.Where(n => n.Name.Equals(nameFilter)).ToList();

        }
        public async Task OnPostAsync()
        {
            insertableAddress = await addessRepository.GetSingle(Convert.ToInt32(insertAddressId));
            if (insertableAddress == null)
                return;
            insertableRoaster = await roasterRepository.GetSingle(Convert.ToInt32(insertRoasterId));
            if (insertableRoaster == null)
                return;
            insertableRoaster.OfficeAddressId =Convert.ToInt32(insertAddressId);
        }
    }
}
