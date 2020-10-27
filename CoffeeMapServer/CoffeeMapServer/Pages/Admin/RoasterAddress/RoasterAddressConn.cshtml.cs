using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        [BindProperty(SupportsGet = true)]
        public string IdFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string NameFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string AddressIdFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string AddressStrFilter { get; set; }

        public List<Address> Addresses { get; set; }

        public List<Roaster> Roasters { get; set; }

        public Roaster InsertableRoaster { get; set; }

        public Address InsertableAddress { get; set; }

        [BindProperty(SupportsGet = true)]
        public string InsertRoasterId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string InsertAddressId { get; set; }

        public string Role { get; set; }

        public string Nickname { get; set; }

        public async Task OnGetAsync()
        {
            Nickname = HttpContext.Request.Cookies[".AspNetCore.Meta.Metadta.nickname"].ToString();
            Role = HttpContext.Request.Cookies[".AspNetCore.Meta.Metadta.role"].ToString();
            Addresses = await addessRepository.GetList();
            Roasters = await roasterRepository.GetList();
            if (!string.IsNullOrEmpty(AddressIdFilter))
                Addresses = Addresses.Where(n => n.Id.Equals(Guid.Parse(AddressIdFilter))).ToList();
            if (!string.IsNullOrEmpty(AddressStrFilter))
                Addresses = Addresses.Where(n => n.AddressStr.Contains(AddressStrFilter)).ToList();
            if (!string.IsNullOrEmpty(IdFilter))
                Roasters = Roasters.Where(n => n.Id.Equals(Guid.Parse(IdFilter))).ToList();
            if (!string.IsNullOrEmpty(NameFilter))
                Roasters = Roasters.Where(n => n.Name.Contains(NameFilter)).ToList();

        }

        public async Task<IActionResult> OnPostAsync()
        {
            InsertableAddress = await addessRepository.GetSingle(Guid.Parse(InsertAddressId));
            if (InsertableAddress == null)
                return RedirectToPage("RoasterAddressConn");
            InsertableRoaster = await roasterRepository.GetSingle(Guid.Parse(InsertRoasterId));
            if (InsertableRoaster == null)
                return RedirectToPage("RoasterAddressConn");
            InsertableRoaster.OfficeAddressId = Guid.Parse(InsertAddressId);
            await roasterRepository.Update(InsertableRoaster);
            return RedirectToPage("RoasterAddressConn");
        }
    }
}
