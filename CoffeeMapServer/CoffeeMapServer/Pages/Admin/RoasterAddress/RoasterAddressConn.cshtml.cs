using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoffeeMapServer.Pages.Admin.RoasterAddress
{
    public class RoasterAddressConnModel : PageModel
    {
        private readonly IRoasterRepository roasterRepository;
        private readonly IAddessRepository addessRepository;
        public RoasterAddressConnModel(IRoasterRepository rRepository, IAddessRepository aRepository)
        {
            roasterRepository = rRepository;
            addessRepository = aRepository;
        }
        [BindProperty(SupportsGet =true)]
        public string idFilter { get; set; }
        [BindProperty(SupportsGet = true)]
        public string nameFilter { get; set; }
        [BindProperty(SupportsGet = true)]
        public string addressIdFilter { get; set; }
        [BindProperty(SupportsGet = true)]
        public string addressStrFilter { get; set; }
        public List<Address> addresses { get; set; }
        public List<Roaster> roasters { get; set; }

        public Roaster insertableRoaster { get; set; }
        public Address insertableAddress { get; set; }

        [BindProperty(SupportsGet = true)]
        public string insertRoasterId { get; set; }
        [BindProperty(SupportsGet = true)]
        public string insertAddressId { get; set; }
        public string role { get; set; }
        public string nickname { get; set; }
        public async Task OnGetAsync()
        {
            nickname = HttpContext.Request.Cookies[".AspNetCore.Meta.Metadta.nickname"].ToString();
            role = HttpContext.Request.Cookies[".AspNetCore.Meta.Metadta.role"].ToString();
            addresses = await addessRepository.GetList();
            roasters = await roasterRepository.GetList();
            if (!string.IsNullOrEmpty(addressIdFilter))
                addresses = addresses.Where(n => n.Id.Equals(Convert.ToInt32(addressIdFilter))).ToList();
            if (!string.IsNullOrEmpty(addressStrFilter))
                addresses = addresses.Where(n => n.AddressStr.Contains(addressStrFilter)).ToList();
            if (!string.IsNullOrEmpty(idFilter))
                roasters = roasters.Where(n => n.Id.Equals(Convert.ToInt32(idFilter))).ToList();
            if (!string.IsNullOrEmpty(nameFilter))
                roasters = roasters.Where(n => n.Name.Contains(nameFilter)).ToList();

        }
        public async Task<IActionResult> OnPostAsync()
        {
            insertableAddress = await addessRepository.GetSingle(Convert.ToInt32(insertAddressId));
            if (insertableAddress == null)
                return RedirectToPage("RoasterAddressConn");
            insertableRoaster = await roasterRepository.GetSingle(Convert.ToInt32(insertRoasterId));
            if (insertableRoaster == null)
                return RedirectToPage("RoasterAddressConn");
            insertableRoaster.OfficeAddressId = Convert.ToInt32(insertAddressId);
            await roasterRepository.Update(insertableRoaster);
            return RedirectToPage("RoasterAddressConn");
        }
    }
}
