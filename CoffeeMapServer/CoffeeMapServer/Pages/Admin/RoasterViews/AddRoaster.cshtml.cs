using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using CoffeeMapServer.Models.Intermediary_models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CoffeeMapServer.Views.Admin.RoasterViews
{
    public class AddRoasterModel : PageModel
    {
        private readonly IRoasterRepository roasterRepository;
        private readonly IAddessRepository addessRepository;
        private readonly ITagRepository tagRepository;
        private readonly IRoasterTagRepository roasterTagRepository;

        [BindProperty]
        public Roaster Roaster { get; set; }

        [BindProperty]
        public IFormFile Picture { get; set; }

        [BindProperty]
        public Address Address { get; set; }

        [BindProperty]
        public string Tags { get; set; }

        public string Role { get; set; }

        public string Nickname { get; set; }

        public AddRoasterModel(IRoasterRepository repository, IAddessRepository addrRepository, ITagRepository tagsRepository, IRoasterTagRepository roasterTagsRepository)
        {
            roasterRepository = repository;
            addessRepository = addrRepository;
            tagRepository = tagsRepository;
            roasterTagRepository = roasterTagsRepository;

        }

        public async Task OnGetAsync()
        {
            Nickname = HttpContext.Request.Cookies[".AspNetCore.Meta.Metadta.nickname"].ToString();
            Role = HttpContext.Request.Cookies[".AspNetCore.Meta.Metadta.role"].ToString();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (await roasterRepository.GetRoasterByName(Roaster.Name) != null)
            {
                return RedirectToPage("Roasters");
            }
            string[] tags_array;
            if (Roaster.ContactEmail == null)
                Roaster.ContactEmail = "none";
            if (Roaster.InstagramProfileLink == null)
                Roaster.InstagramProfileLink = "none";
            if (Roaster.WebSiteLink == null)
                Roaster.WebSiteLink = "none";
            if (Roaster.VkProfileLink == null)
                Roaster.VkProfileLink = "none";
            if (Roaster.TelegramProfileLink == null)
                Roaster.TelegramProfileLink = "none";
            if (Address.OpeningHours == null)
                Address.OpeningHours = "none";
            if (Tags.Length == 0)
            {
                Tags = "none";
                tags_array = Tags.Split("#");
            }
            else
            {
                tags_array = Tags.Split("#");
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
            await addessRepository.Create(Address);
            var addr = await addessRepository.GetSingle(Address);
            Roaster.OfficeAddressId = addr.Id;

            if (Picture != null)
            {
                byte[] bytePicture = null;
                using (var binaryReader = new BinaryReader(Picture.OpenReadStream()))
                {
                    bytePicture = binaryReader.ReadBytes((int)Picture.Length);
                }
                Roaster.Picture = bytePicture;
            }
            await roasterRepository.Create(Roaster);
            var roasterId = (await roasterRepository.GetRoaster(Roaster)).Id;
            foreach (var i in _localTags)
            {
                await roasterTagRepository.Create(new RoasterTag { RoasterId = roasterId, TagId = i.Id });
            }
            return RedirectToPage("Roasters");

        }
    }
}
