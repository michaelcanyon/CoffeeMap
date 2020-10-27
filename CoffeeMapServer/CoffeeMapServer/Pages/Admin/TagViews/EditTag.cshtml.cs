using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace CoffeeMapServer.Pages.Admin.TagViews
{
    public class EditTagModel : PageModel
    {
        private readonly ITagRepository tagRepository;

        [BindProperty]
        public Tag Tag { get; set; }

        public string Nickname { get; set; }

        public string Role { get; set; }

        public Guid Guid { get; set; }

        public EditTagModel(ITagRepository tagsRepository)
        {
            tagRepository = tagsRepository;
        }

        public async Task<IActionResult> OnGet(Guid id)
        {
            Guid = id;
            Role = HttpContext.Request.Cookies[".AspNetCore.Meta.Metadta.role"].ToString();
            Nickname = HttpContext.Request.Cookies[".AspNetCore.Meta.Metadta.nickname"].ToString();
            try
            {
                Tag = await tagRepository.GetSingle(id);
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
                await tagRepository.Update(Tag);
                return RedirectToPage("Tags");
            }
            catch
            {
                return RedirectToPage("Tags");
            }
        }
    }
}
