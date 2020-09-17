using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoffeeMapServer.Pages.Admin.TagViews
{
    public class EditTagModel : PageModel
    {
        private readonly ITagRepository tagRepository;

        [BindProperty]
        public Tag tag { get; set; }
        public string nickname { get; set; }
        public string role { get; set; }
        public EditTagModel(ITagRepository tagsRepository)
        {
            tagRepository = tagsRepository;
        }
        public async Task<IActionResult> OnGet(int? id)
        {
            role = HttpContext.Request.Cookies[".AspNetCore.Meta.Metadta.role"].ToString();
            nickname = HttpContext.Request.Cookies[".AspNetCore.Meta.Metadta.nickname"].ToString();
            try
            {
                tag = await tagRepository.GetSingle(Convert.ToInt32(id));
                return Page();
            }
            catch {
                return RedirectToPage("Tags");
            }
        }
        public async Task<IActionResult> OnPostProcessAsync()
        {
            try
            {
                await tagRepository.Update(tag);
                return RedirectToPage("Tags");
            }
            catch {
                return RedirectToPage("Tags");
            }
        }
    }
}
