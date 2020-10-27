using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoffeeMapServer.Pages.Admin.TagViews
{
    public class TagsModel : PageModel
    {
        private readonly ITagRepository TagRepository;

        public List<Tag> Tags { get; set; }

        public string Nickname { get; set; }

        public string Role { get; set; }

        public TagsModel(ITagRepository tagRepository)
        {
            TagRepository = tagRepository;
        }

        public async Task OnGetAsync()
        {
            Role = HttpContext.Request.Cookies[".AspNetCore.Meta.Metadta.role"].ToString();
            Nickname = HttpContext.Request.Cookies[".AspNetCore.Meta.Metadta.nickname"].ToString();
            Tags = await TagRepository.GetList();
        }
    }
}
