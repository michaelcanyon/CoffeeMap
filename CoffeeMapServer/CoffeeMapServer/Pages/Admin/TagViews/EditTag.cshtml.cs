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

        [BindProperty]
        public string RStatusCode { get; set; }
        public EditTagModel(ITagService tagService)
            => _tagService = tagService ?? throw new ArgumentNullException(nameof(ITagService));

        public async Task<IActionResult> OnGet(Guid id, string statusCode)
        {
            RStatusCode = statusCode;
            Role = HttpContext.Request.Cookies[".AspNetCore.Meta.Metadata.role"].ToString();
            Nickname = HttpContext.Request.Cookies[".AspNetCore.Meta.Metadata.nickname"].ToString();
            Tag = await _tagService.FetchSingleTagAsync(id);
            if (Tag == null)
                return BadRequest();
            return Page();
        }

        public async Task<IActionResult> OnPostProcessAsync()
        {
            var respCode = await _tagService.UpdateSingleTagAsync(Tag);
            if (respCode.Equals(0))
                return RedirectToPage("Tags");
            else if (respCode.Equals(-1))
                return RedirectToPage("EditTag",new { id = Tag.Id, statusCode = "601" });
            else
                return BadRequest();
        }
    }
}