using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using CoffeeMapServer.Models.Intermediary_models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeMapServer.Views.Admin.RoasterViews
{
    public class EditRoasterModel : PageModel
    {
        private readonly IRoasterRepository roasterRepository;
        private readonly IRoasterTagRepository roasterTagRepository;
        private readonly ITagRepository tagRepository;

        public Guid Guid { get; set; }

        [BindProperty]
        public IFormFile Picture { get; set; }  

        [BindProperty]
        public Roaster Roaster { get; set; }

        [BindProperty]
        public string Tags { get; set; }

        [BindProperty]
        public string DeletableTags { get; set; }

        public string Role { get; set; }

        public List<string> tagsList = new List<string>();

        public List<string> DeletableTagsList = new List<string>();


        public EditRoasterModel(IRoasterRepository repository, IRoasterTagRepository roasterTagsRepository, ITagRepository tagsRepository)
        {
            roasterRepository = repository;
            roasterTagRepository = roasterTagsRepository;
            tagRepository = tagsRepository;
        }

        public async Task<IActionResult> OnGet(Guid id)
        {
            Role = HttpContext.Request.Cookies[".AspNetCore.Meta.Metadta.role"].ToString();
            Roaster = await roasterRepository.GetSingle(id);
            var currentTagPairs = await roasterTagRepository.GetPairsByRoasterId(Roaster.Id);
            foreach (var i in currentTagPairs)
                tagsList.Add((await tagRepository.GetSingle(i.TagId)).TagTitle);
            if (Roaster.ContactEmail == "none")
                Roaster.ContactEmail = null;
            if (Roaster.InstagramProfileLink == "none")
                Roaster.InstagramProfileLink = null;
            if (Roaster.TelegramProfileLink == "none")
                Roaster.TelegramProfileLink = null;
            if (Roaster.VkProfileLink == "none")
                Roaster.VkProfileLink = null;
            if (Roaster.WebSiteLink == "none")
                Roaster.WebSiteLink = null;
            return Page();
        }

        public async Task<IActionResult> OnPostProcessAsync()
        {
            if (Tags != null && Tags.Length > 0)
            {
                var addTagsList = Tags.Split("#");
                foreach (var i in addTagsList)
                    if (!tagsList.Contains(i))
                        tagsList.Add(i);
                if (tagsList.Contains(""))
                    tagsList.Remove("");
            }

            if (DeletableTags != null && DeletableTags.Length > 0)
                DeletableTagsList.AddRange(DeletableTags.Split("#"));
            if (DeletableTagsList.Count() > 0)
                if (DeletableTagsList.Contains(""))
                    DeletableTagsList.Remove("");
            foreach (var i in DeletableTagsList)
            {
                var currentTag = await tagRepository.GetSingle(i);
                await roasterTagRepository.Delete(Roaster.Id, currentTag.Id);
            }

            var pairs = await roasterTagRepository.GetPairsByRoasterId(Roaster.Id);
            foreach (var i in tagsList)
            {
                Tag currentTag;
                currentTag = await tagRepository.GetSingle(i);

                if (currentTag == null)
                {
                    await tagRepository.Create(new Tag { TagTitle = i });
                    currentTag = await tagRepository.GetSingle(i);
                }

                var buffRoasterTags = pairs.Where(item => item.TagId == currentTag.Id);
                if (buffRoasterTags.Count() == 0)
                    await roasterTagRepository.Create(new RoasterTag { RoasterId = Roaster.Id, TagId = currentTag.Id });
            }
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

            if (Picture != null)
            {
                byte[] bytePicture = null;
                using (var binaryReader = new BinaryReader(Picture.OpenReadStream()))
                {
                    bytePicture = binaryReader.ReadBytes((int)Picture.Length);
                }
                Roaster.Picture = bytePicture;
            }

            await roasterRepository.Update(Roaster);
            return RedirectToPage("Roasters");
        }
    }
}
