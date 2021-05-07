using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeMapServer.Models;
using CoffeeMapServer.Services.Interfaces.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoffeeMapServer.Pages.Admin.RoasterAddress
{
    public class RoasterAddressConnModel : PageModel
    {
        private readonly IRoasterAddressConnectionService _roasterAddressConnectionService;

        public RoasterAddressConnModel(IRoasterAddressConnectionService roasterAddressConnectionService)
             => _roasterAddressConnectionService = roasterAddressConnectionService ?? throw new ArgumentNullException(nameof(IRoasterAddressConnectionService));

        [BindProperty(SupportsGet = true)]
        public string IdFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string NameFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string AddressIdFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string AddressStrFilter { get; set; }

        public IList<Address> Addresses { get; set; }

        public IList<Roaster> Roasters { get; set; }

        public Roaster InsertableRoaster { get; set; }

        public Address InsertableAddress { get; set; }

        [BindProperty(SupportsGet = true)]
        public string InsertRoasterId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string InsertAddressId { get; set; }

        public string Role { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                Role = HttpContext.Request.Cookies[".AspNetCore.Meta.Metadata.role"].ToString();
                Addresses = await _roasterAddressConnectionService.FetchAddressesAsync();
                Roasters = await _roasterAddressConnectionService.FetchRoastersAsync();
                if (!string.IsNullOrEmpty(AddressIdFilter))
                    Addresses = Addresses
                                .Where(n => n.Id.Equals(Guid.Parse(AddressIdFilter)))
                                .ToList();
                if (!string.IsNullOrEmpty(AddressStrFilter))
                    Addresses = Addresses
                                .Where(n => n.AddressStr.Contains(AddressStrFilter))
                                .ToList();
                if (!string.IsNullOrEmpty(IdFilter))
                    Roasters = Roasters
                               .Where(n => n.Id.Equals(Guid.Parse(IdFilter)))
                               .ToList();
                if (!string.IsNullOrEmpty(NameFilter))
                    Roasters = Roasters
                               .Where(n => n.Name.Contains(NameFilter))
                               .ToList();
                return Page();
            }
            catch
            {
                return BadRequest();
            }

        }

        public async Task<IActionResult> OnPostAsync()
        {
            InsertableAddress = await _roasterAddressConnectionService.FetchSingleAddressByIdAsync(Guid.Parse(InsertAddressId));
            InsertableRoaster = await _roasterAddressConnectionService.FetchSingleRoasterByIdAsync(Guid.Parse(InsertRoasterId));

            if (InsertableAddress == null || InsertableRoaster == null)
                return BadRequest();

            InsertableRoaster.OfficeAddress = InsertableAddress;
            var respCode = await _roasterAddressConnectionService.UpdateRoasterAsync(InsertableRoaster);

            return respCode.Equals(0) ? RedirectToPage("RoasterAddressConn") : (IActionResult)BadRequest();
        }
    }
}