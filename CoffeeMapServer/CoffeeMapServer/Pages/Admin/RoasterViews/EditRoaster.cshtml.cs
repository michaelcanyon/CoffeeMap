using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeMapServer.Models;
using CoffeeMapServer.Services.Interfaces.Admin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoffeeMapServer.Views.Admin.RoasterViews
{
    public class EditRoasterModel : PageModel
    {
        private readonly IRoasterAdminService _roasterAdminService;

        [BindProperty]
        public IFormFile Picture { get; set; }

        [BindProperty]
        public Roaster Roaster { get; set; }

        [BindProperty]
        public string TagsToAdd { get; set; }

        [BindProperty]
        public string TagsToDelete { get; set; }

        public string Role { get; set; }

        public List<string> tagsList = new List<string>();

        public List<string> DeletableTagsList = new List<string>();

        [BindProperty]
        public string RStatusCode { get; set; }

        public EditRoasterModel(IRoasterAdminService roasterAdminService)
            => _roasterAdminService = roasterAdminService ?? throw new ArgumentNullException(nameof(IRoasterAdminService));

        public async Task<IActionResult> OnGet(Guid id, string statusCode)
        {
            RStatusCode = statusCode;
            Role = HttpContext.Request.Cookies[".AspNetCore.Meta.Metadata.role"].ToString();
            Roaster = await _roasterAdminService.FetchSingleRoasterAsync(id);
            tagsList.AddRange(Roaster.RoasterTags.Select(t => t.Tag.TagTitle).ToList());
            if (Roaster == null)
                return BadRequest();
            return Page();
        }

        public async Task<IActionResult> OnPostProcessAsync()
        {
           var respCode= await _roasterAdminService.UpdateRoasterAsync(Roaster, TagsToAdd, TagsToDelete, Picture);
            if (respCode.Equals(0))
                return RedirectToPage("Roasters");
            else if (respCode.Equals(-1))
                return RedirectToPage("EditRoaster",new { id=Roaster.Id, statusCode="601" });
            else
                return BadRequest();
        }
    }
}