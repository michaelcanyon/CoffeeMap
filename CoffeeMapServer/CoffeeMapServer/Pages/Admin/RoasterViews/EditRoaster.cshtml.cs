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
        public string tagPart { get; set; }

        public List<string> tagsList = new List<string>();
        public List<string> DeletableTagsList = new List<string>();


        public EditRoasterModel(IRoasterRepository repository, IRoasterTagRepository roasterTagsRepository, ITagRepository tagsRepository)
        {
            roasterRepository = repository;
            roasterTagRepository = roasterTagsRepository;
            tagRepository = tagsRepository;
        }
        public async Task<IActionResult> OnGet(int? id)
        {
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
        public async Task<IActionResult> OnPostAsync()
        {
            Tag currentTag;
            if(DeletableTagsList.Count()>0)
            foreach (var i in DeletableTagsList)
            {
                currentTag= await tagRepository.GetSingle(i);
                await roasterTagRepository.Delete(roaster.Id, currentTag.Id);
            }
            foreach (var i in tagsList)
            {
                currentTag = null;
                currentTag = await tagRepository.GetSingle(i);
                if (currentTag == null)
                { 
                    await tagRepository.Create(currentTag);
                  currentTag = await tagRepository.GetSingle(i);
                }
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
        public void OnPostDeleteTag()
        {
            tagsList.Remove(tagPart);
            DeletableTagsList.Add(tagPart);
        }
        public void OnPostAddTag()
        {
            tagsList.Add(tagPart);
        }
    }
}
