using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeMapServer.Models;
using CoffeeMapServer.Services.Interfaces.Admin;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoffeeMapServer.Pages.Admin.TagViews
{
    public class TagsModel : PageModel
    {
        private readonly ITagService _tagService;

        public IList<Tag> Tags { get; set; }

        public string Nickname { get; set; }

        public string Role { get; set; }

        public TagsModel(ITagService tagService)
            => _tagService = tagService;

        public async Task OnGetAsync()
        {
            Role = HttpContext.Request.Cookies[".AspNetCore.Meta.Metadta.role"].ToString();
            Nickname = HttpContext.Request.Cookies[".AspNetCore.Meta.Metadta.nickname"].ToString();
            Tags = await _tagService.FetchTagsListAsync();
        }
    }
}
