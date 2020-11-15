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

        public EditTagModel(ITagService tagService)
            => _tagService = tagService ?? throw new ArgumentNullException(nameof(ITagService));

        public async Task<IActionResult> OnGet(Guid guid)
        {
            Role = HttpContext.Request.Cookies[".AspNetCore.Meta.Metadata.role"].ToString();
            Nickname = HttpContext.Request.Cookies[".AspNetCore.Meta.Metadata.nickname"].ToString();
            try
            {
                Tag = await _tagService.FetchSingleTagAsync(guid);
                return Page();
            }
            catch
            { 
                return RedirectToPage("Tags"); 
            }
        }

        public async Task<IActionResult> OnPostProcessAsync()
        {
            try
            {
                await _tagService.UpdateSingleTagAsync(Tag);
                return RedirectToPage("Tags");
            }
            catch
            { 
                return RedirectToPage("Tags"); 
            }
        }
    }
}