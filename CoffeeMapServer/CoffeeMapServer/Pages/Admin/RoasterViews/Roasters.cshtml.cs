using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeMapServer.Models;
using CoffeeMapServer.Models.Intermediary_models;
using CoffeeMapServer.Services.Interfaces.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoffeeMapServer.Views.Admin.RoasterViews
{
    public class RoastersModel : PageModel
    {
        private readonly IRoasterAdminService _roasterAdminService;
        public IList<Roaster> Roasters { get; set; }
        public IList<RoasterTag> RoasterTags { get; set; }

        public IList<Tag> Tags { get; set; }

        [BindProperty(SupportsGet = true)]
        public string IdFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string NameFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string OfficeAddressIdFilter { get; set; }

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

        public RoastersModel(IRoasterAdminService roasterAdminService)
            => _roasterAdminService = roasterAdminService;


        // TODO: проверить, где состояния не нужно хранить в Pages. P.S Подумал. Нужны для подгрузки меню мастера\администратора. Несколько состояний, действительно, были лишними.
        public string Role { get; set; }

        public async Task OnGetAsync()
        {
            Role = HttpContext.Request.Cookies[".AspNetCore.Meta.Metadata.role"].ToString();
            Roasters = await _roasterAdminService.FetchRoastersAsync();
            RoasterTags = await _roasterAdminService.FetchRoasterTagsAsync();
            Tags = await _roasterAdminService.FetchTagsAsync();
            // TODO: подумать над бизнес-сценариями
            if (!string.IsNullOrEmpty(IdFilter))
                Roasters = Roasters.Where(s => s.Id.Equals(IdFilter)).ToList();
            if (!string.IsNullOrEmpty(NameFilter))
                Roasters = Roasters.Where(s => s.Name.Contains(NameFilter)).ToList();
            if (!string.IsNullOrEmpty(OfficeAddressIdFilter))
                Roasters = Roasters.Where(s => s.OfficeAddressId.Equals(OfficeAddressIdFilter)).ToList();
            if (!string.IsNullOrEmpty(ContactNumberFilter))
                Roasters = Roasters.Where(s => s.ContactNumber.Contains(ContactNumberFilter)).ToList();
            if (!string.IsNullOrEmpty(ContactEmailFilter))
                Roasters = Roasters.Where(s => s.ContactEmail.Contains(ContactEmailFilter)).ToList();
            if (!string.IsNullOrEmpty(InstagramProfileFilter))
                Roasters = Roasters.Where(s => s.InstagramProfileLink.Contains(InstagramProfileFilter)).ToList();
            if (!string.IsNullOrEmpty(VkProfileFilter))
                Roasters = Roasters.Where(s => s.VkProfileLink.Contains(VkProfileFilter)).ToList();
            if (!string.IsNullOrEmpty(TelegramProfileFilter))
                Roasters = Roasters.Where(s => s.TelegramProfileLink.Contains(TelegramProfileFilter)).ToList();
            //TODO: Check if following codelines affect on results
            if (!string.IsNullOrEmpty(TagString))
            {
                var tagsArr = TagString.Split(" ");
                foreach (var i in tagsArr)
                {
                    if (i == "")
                        continue;
                    Tags = Tags.Where(tag => tag.TagTitle.Contains(i)).ToList();
                }
            }
        }

    }
}
