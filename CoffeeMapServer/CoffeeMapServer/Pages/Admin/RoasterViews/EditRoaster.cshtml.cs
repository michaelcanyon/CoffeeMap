using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
        public string Tags { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Latitude { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Longitude { get; set; }

        public string Role { get; set; }

        public List<string> tagsList = new List<string>();

        [BindProperty]
        public string RStatusCode { get; set; }

        public EditRoasterModel(IRoasterAdminService roasterAdminService)
            => _roasterAdminService = roasterAdminService ?? throw new ArgumentNullException(nameof(IRoasterAdminService));

        public async Task<IActionResult> OnGet(Guid id, string statusCode)
        {
            RStatusCode = statusCode;
            Role = HttpContext.Request.Cookies[".AspNetCore.Meta.Metadata.role"].ToString();
            Roaster = await _roasterAdminService.FetchSingleRoasterAsync(id);
            if (Roaster.OfficeAddress!=null)
            {
                Latitude = Roaster.OfficeAddress.Latitude.ToString();
                Longitude = Roaster.OfficeAddress.Longitude.ToString();
                tagsList.AddRange(Roaster.RoasterTags.Select(t => t.Tag.TagTitle).ToList());
            }
            return Roaster == null ? BadRequest() : (IActionResult)Page();
        }

        public async Task<IActionResult> OnPostProcessAsync()
        {
            if (!String.IsNullOrEmpty(Tags))
            {
                Tags = Regex.Replace(Tags, @"[{:}#]", "");
                Tags = Tags.Replace("value", "")
                           .Replace("[", "")
                           .Replace("]", "")
                           .Replace(@"\", string.Empty)
                           .Replace("\"", string.Empty)
                           .Replace(",", "#");
            }

            var respCode = await _roasterAdminService.UpdateRoasterAsync(Roaster,
                                                                       Tags,
                                                                       Latitude,
                                                                       Longitude,
                                                                       Picture);
            return respCode.Equals(0)
                ? RedirectToPage("Roasters")
                : respCode.Equals(-1) ? RedirectToPage("EditRoaster", new { id = Roaster.Id, statusCode = "601" }) : (IActionResult)BadRequest();
        }
    }
}