using System;
using System.Threading.Tasks;
using CoffeeMapServer.Services.Interfaces.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoffeeMapServer.Pages.Admin.TagViews
{
    public class DeleteTagModel : PageModel
    {
        private readonly ITagService _tagService;

        public Guid Guid { get; set; }

        public DeleteTagModel(ITagService tagService)
            => _tagService = tagService;

        public async Task<IActionResult> OnGet(Guid id)
        {
            Guid = id;
            try
            {
                await _tagService.DeleteTagAsync(Guid);
                return RedirectToPage("Tags");
            }
            catch
            {
                return RedirectToPage("Tags");
            }
        }
    }
}
