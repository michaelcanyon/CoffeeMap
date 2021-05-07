using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeMapServer.Models;
using CoffeeMapServer.Services.Interfaces.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;
using SerilogTimings;

namespace CoffeeMapServer.Views.Admin.RoasterViews
{
    public class RoastersModel : PageModel
    {
        private readonly IRoasterAdminService _roasterAdminService;

        private ILogger _logger;
        public IList<Roaster> Roasters { get; set; }

        [BindProperty(SupportsGet = true)]
        public string IdFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string NameFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string OfficeAddressFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string ContactNumberFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string ContactEmailFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string InstagramProfileFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string TelegramProfileFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string VkProfileFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string WebSiteFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string TagString { get; set; }

        public RoastersModel(IRoasterAdminService roasterAdminService, ILogger logger)
             {_roasterAdminService = roasterAdminService ?? throw new ArgumentNullException(nameof(IRoasterAdminService));
        _logger = logger;
    }

        public string Role { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
               
                    Role = HttpContext.Request.Cookies[".AspNetCore.Meta.Metadata.role"].ToString();
                using (Operation.Time("Fetching roasters..."))
                {
                    Roasters = await _roasterAdminService.FetchRoastersAsync();
                }
                if (!string.IsNullOrEmpty(IdFilter))
                        Roasters = Roasters
                                   .Where(s => s.Id.ToString().Equals(IdFilter))
                                   .ToList();
                    if (!string.IsNullOrEmpty(NameFilter))
                        Roasters = Roasters
                                   .Where(s => s.Name
                                                .Contains(NameFilter))
                                   .ToList();
                    if (!string.IsNullOrEmpty(OfficeAddressFilter))
                        Roasters = Roasters
                                   .Where(s => s.OfficeAddress.AddressStr
                                                              .Contains(OfficeAddressFilter))
                                   .ToList();
                    if (!string.IsNullOrEmpty(ContactNumberFilter))
                        Roasters = Roasters
                                   .Where(s => s.ContactNumber
                                                .Contains(ContactNumberFilter))
                                   .ToList();
                    if (!string.IsNullOrEmpty(ContactEmailFilter))
                        Roasters = Roasters
                                   .Where(s => s.ContactEmail
                                                .Contains(ContactEmailFilter))
                                   .ToList();
                    if (!string.IsNullOrEmpty(InstagramProfileFilter))
                        Roasters = Roasters
                                   .Where(s => s.InstagramProfileLink
                                                .Contains(InstagramProfileFilter))
                                   .ToList();
                    if (!string.IsNullOrEmpty(VkProfileFilter))
                        Roasters = Roasters.
                                   Where(s => s.VkProfileLink
                                               .Contains(VkProfileFilter))
                                   .ToList();
                    if (!string.IsNullOrEmpty(TelegramProfileFilter))
                        Roasters = Roasters
                                   .Where(s => s.TelegramProfileLink
                                                .Contains(TelegramProfileFilter))
                                   .ToList();
                    if (!string.IsNullOrEmpty(TagString))
                        Roasters = Roasters
                                   .SelectMany(r => r.RoasterTags, (r, t) => new { roast = r, tagp = t })
                                   .Where(pa => pa.tagp.Tag.TagTitle
                                                            .Contains(TagString))
                                   .Select(pa => pa.roast)
                                   .ToList();
                return Page();
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}