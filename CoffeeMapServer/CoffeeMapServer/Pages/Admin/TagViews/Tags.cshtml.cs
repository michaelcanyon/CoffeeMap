using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using CoffeeMapServer.Models.Intermediary_models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoffeeMapServer.Pages.Admin.TagViews
{
    public class TagsModel : PageModel
    {
        private readonly ITagRepository TagRepository;

        public List<Tag> tags { get; set; }
        public TagsModel(ITagRepository tagRepository)
        {
            TagRepository = tagRepository;
        }
        public async Task OnGetAsync()
        {
            tags = await TagRepository.GetList();
        }
    }
}
