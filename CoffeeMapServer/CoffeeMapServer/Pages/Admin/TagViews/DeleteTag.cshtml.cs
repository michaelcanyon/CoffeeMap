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

        public DeleteTagModel(ITagService tagService)
            => _tagService = tagService ?? throw new ArgumentNullException(nameof(ITagService));

        public async Task<IActionResult> OnGet(Guid id)
        {
            var respCode = await _tagService.DeleteTagAsync(id);
            if (respCode.Equals(0))
                return RedirectToPage("Tags");
            else
                return BadRequest();
        }
    }
}