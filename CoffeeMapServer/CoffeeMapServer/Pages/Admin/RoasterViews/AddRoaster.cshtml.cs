using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using CoffeeMapServer.Models.Intermediary_models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoffeeMapServer.Views.Admin.RoasterViews
{
    public class AddRoasterModel : PageModel
    {
        private readonly IRoasterRepository roasterRepository;
        private readonly IAddessRepository addessRepository;
        private readonly ITagRepository tagRepository;
        private readonly IRoasterTagRepository roasterTagRepository;

        [BindProperty]
        public Roaster roaster { get; set; }

        [BindProperty]
        public Address address { get; set; }

        [BindProperty]
        public string tags { get; set; }
        public string role { get; set; }
        public string nickname { get; set; }
        public AddRoasterModel(IRoasterRepository repository, IAddessRepository addrRepository, ITagRepository tagsRepository, IRoasterTagRepository roasterTagsRepository)
        {
            roasterRepository = repository;
            addessRepository = addrRepository;
            tagRepository = tagsRepository;
            roasterTagRepository = roasterTagsRepository;

        }
        public async Task OnGetAsync()
        {
            nickname = HttpContext.Request.Cookies[".AspNetCore.Meta.Metadta.nickname"].ToString();
            role = HttpContext.Request.Cookies[".AspNetCore.Meta.Metadta.role"].ToString();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (await roasterRepository.GetRoasterByName(roaster.Name) != null)
            {
                return RedirectToPage("Roasters");
            }
            string[] tags_array;
            if (roaster.ContactEmail == null)
                roaster.ContactEmail = "none";
            if (roaster.InstagramProfileLink == null)
                roaster.InstagramProfileLink = "none";
            if (roaster.WebSiteLink == null)
                roaster.WebSiteLink = "none";
            if (roaster.VkProfileLink == null)
                roaster.VkProfileLink = "none";
            if (roaster.TelegramProfileLink == null)
                roaster.TelegramProfileLink = "none";
            if (address.OpeningHours == null)
                address.OpeningHours = "none";
            if (tags.Length == 0)
            {
                tags = "none";
                tags_array = tags.Split("#");
            }
            else
            { 
                tags_array = tags.Split("#");
                foreach (var i in tags_array)
                {
                    if (i == "")
                        continue;
                    if ((await tagRepository.GetSingle(i)) == null)
                        await tagRepository.Create(new Tag { TagTitle = i });
                }
            }
            List<Tag> _localTags = new List<Tag>();
            foreach (var i in tags_array)
            {
                if (i == "")
                    continue;
                _localTags.Add(await tagRepository.GetSingle(i));
            }
            await addessRepository.Create(address);
            var addr = await addessRepository.GetSingle(address);
            roaster.OfficeAddressId = addr.Id;
            await roasterRepository.Create(roaster);
            var roasterId = (await roasterRepository.GetRoaster(roaster)).Id;
            foreach (var i in _localTags)
            {
                await roasterTagRepository.Create(new RoasterTag { RoasterId = roasterId, TagId = i.Id });
            }
            return RedirectToPage("Roasters");

        }
    }
}
