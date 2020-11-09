using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeMapServer.Models;
using CoffeeMapServer.Services.Interfaces.Admin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoffeeMapServer.Views.Admin.RoasterViews
{
    public class EditRoasterModel : PageModel
    {
        private readonly IRoasterAdminService _roasterAdminService;

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


        public EditRoasterModel(IRoasterAdminService roasterAdminService)
            => _roasterAdminService = roasterAdminService;

        public async Task<IActionResult> OnGet(Guid id)
        {
            Role = HttpContext.Request.Cookies[".AspNetCore.Meta.Metadata.role"].ToString();
            Roaster = await _roasterAdminService.FetchSingleRoasterAsync(id);
            var currentTagPairs = await _roasterAdminService.FetchRoasterTagsAsync(Roaster.Id);
            foreach (var i in currentTagPairs)
                tagsList.Add((await _roasterAdminService.FetchTagByIdAsync(i.TagId)).TagTitle);
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
            await _roasterAdminService.UpdateRoasterAsync(Roaster, Tags, DeletableTags, Picture);
            return RedirectToPage("Roasters");
        }
    }
}
