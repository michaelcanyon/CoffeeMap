using System;
using System.Threading.Tasks;
using CoffeeMapServer.Models;
using CoffeeMapServer.Services.Interfaces.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoffeeMapServer.Pages.Admin.TagViews
{
    public class EditTagModel : PageModel
    {
        private readonly ITagService _tagService;

        [BindProperty]
        public Tag Tag { get; set; }

        public string Nickname { get; set; }

        public string Role { get; set; }

        public Guid Guid { get; set; }

        public EditTagModel(ITagService tagService)
            => _tagService = tagService;

        public async Task<IActionResult> OnGet(Guid id)
        {
            Guid = id;
            Role = HttpContext.Request.Cookies[".AspNetCore.Meta.Metadta.role"].ToString();
            Nickname = HttpContext.Request.Cookies[".AspNetCore.Meta.Metadta.nickname"].ToString();
            try
            {
                Tag = await _tagService.FetchSingleTagAsync(id);
                return Page();
            }
            catch
            { return RedirectToPage("Tags"); }
        }

        public async Task<IActionResult> OnPostProcessAsync()
        {
            try
            {
                await _tagService.UpdateSingleTagAsync(Tag);
                return RedirectToPage("Tags");
            }
            catch
            { return RedirectToPage("Tags"); }
        }
    }
}
