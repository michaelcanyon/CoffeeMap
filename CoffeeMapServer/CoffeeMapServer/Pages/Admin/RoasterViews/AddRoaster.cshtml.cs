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
        public string tagPart { get; set; }

        private StringBuilder tags = new StringBuilder();

        public AddRoasterModel(IRoasterRepository repository, IAddessRepository addrRepository, ITagRepository tagsRepository, IRoasterTagRepository roasterTagsRepository)
        {
            roasterRepository = repository;
            addessRepository = addrRepository;
            tagRepository = tagsRepository;
            roasterTagRepository = roasterTagsRepository;

        }
        public async Task<IActionResult> OnPostAsync()
        {
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
                tags.AppendFormat("none");
                tags_array = tags.ToString().Split("#");
            }
            else
            { // TODO: ������ ���������� � ������� �����
                tags_array = tags.ToString().Split("#");
                foreach (var i in tags_array)
                {
                    if (tagRepository.GetSingle(i) != null)
                        await tagRepository.Create(new Tag { TagTitle = i });
                }
            }
            List<Tag> _localTags = new List<Tag>();
            foreach (var i in tags_array)
            {
                _localTags.Add( await tagRepository.GetSingle(i));
            }
            await addessRepository.Create(address);
            var addr = await addessRepository.GetSingle(address);
            roaster.OfficeAddressId = addr.Id;
            await roasterRepository.Create(roaster);
            var roasterId = (await roasterRepository.GetRoasterId(roaster)).Id;
            foreach (var i in _localTags)
            {
               await roasterTagRepository.Create(new RoasterTag { RoasterId = roasterId, TagId = i.Id });
            }
            return RedirectToPage("Roasters");

        }
        public void OnPostAddTag()
        {
            tags.AppendFormat(tagPart, "#");
        }
    }
}
