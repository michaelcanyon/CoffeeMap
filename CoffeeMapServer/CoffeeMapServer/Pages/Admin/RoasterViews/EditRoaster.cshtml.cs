using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Infrastructures.Repositories;
using CoffeeMapServer.Models;
using CoffeeMapServer.Models.Intermediary_models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoffeeMapServer.Views.Admin.RoasterViews
{
    public class EditRoasterModel : PageModel
    {
        private readonly IRoasterRepository roasterRepository;
        private readonly IRoasterTagRepository roasterTagRepository;
        private readonly ITagRepository tagRepository;

        [BindProperty]
        public Roaster roaster { get; set; }

        [BindProperty]
        public string tags { get; set; }

        [BindProperty]
        public string deletableTags { get; set; }

        public string role { get; set; }
        public List<string> tagsList = new List<string>();
        public List<string> DeletableTagsList = new List<string>();
        public string nickname { get; set; }


        public EditRoasterModel(IRoasterRepository repository, IRoasterTagRepository roasterTagsRepository, ITagRepository tagsRepository)
        {
            roasterRepository = repository;
            roasterTagRepository = roasterTagsRepository;
            tagRepository = tagsRepository;
        }
        public async Task<IActionResult> OnGet(int? id)
        {
            nickname = HttpContext.Request.Cookies[".AspNetCore.Meta.Metadta.nickname"].ToString();
            role = HttpContext.Request.Cookies[".AspNetCore.Meta.Metadta.role"].ToString();
            roaster = await roasterRepository.GetSingle(Convert.ToInt32(id));
            var currentTagPairs = await roasterTagRepository.GetPairsByRoasterId(roaster.Id);
            foreach (var i in currentTagPairs)
                tagsList.Add((await tagRepository.GetSingle(i.TagId)).TagTitle);
            if (roaster.ContactEmail == "none")
                roaster.ContactEmail = null;
            if (roaster.InstagramProfileLink == "none")
                roaster.InstagramProfileLink = null;
            if (roaster.TelegramProfileLink == "none")
                roaster.TelegramProfileLink = null;
            if (roaster.VkProfileLink == "none")
                roaster.VkProfileLink = null;
            if (roaster.WebSiteLink == "none")
                roaster.WebSiteLink = null;
            return Page();
        }
        public async Task<IActionResult> OnPostProcessAsync()
        {
            if (tags!=null && tags.Length > 0)
            {
                var addTagsList = tags.Split("#");
                foreach (var i in addTagsList)
                    if (!tagsList.Contains(i))
                        tagsList.Add(i);
                if (tagsList.Contains(""))
                    tagsList.Remove("");
            }

            if(deletableTags!=null && deletableTags.Length>0)
            DeletableTagsList.AddRange(deletableTags.Split("#"));
            if (DeletableTagsList.Count() > 0)
                if (DeletableTagsList.Contains(""))
                    DeletableTagsList.Remove("");
                foreach (var i in DeletableTagsList)
                {
                    var currentTag = await tagRepository.GetSingle(i);
                    await roasterTagRepository.Delete(roaster.Id, currentTag.Id);
                }

            var pairs =await roasterTagRepository.GetPairsByRoasterId(roaster.Id);
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
                if (buffRoasterTags.Count()==0)
                await roasterTagRepository.Create(new RoasterTag { RoasterId=roaster.Id, TagId=currentTag.Id});      
            }
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
            await roasterRepository.Update(roaster);
            return RedirectToPage("Roasters");
        }
    }
}
