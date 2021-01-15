using System;
using System.Threading.Tasks;
using CoffeeMapServer.Models;
using CoffeeMapServer.Services.Interfaces.Admin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoffeeMapServer.Pages.Admin.RoasterRequestViews
{
    public class EditRoasterRequestModel : PageModel
    {
        private readonly IRoasterRequestService _roasterRequestService;
        [BindProperty]

        public RoasterRequest request { get; set; }

        [BindProperty]
        public IFormFile Picture { get; set; }

        public string Role { get; set; }

        public EditRoasterRequestModel(IRoasterRequestService roasterRequestService)
            => _roasterRequestService = roasterRequestService ?? throw new ArgumentNullException(nameof(IRoasterRequestService));

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            Role = HttpContext.Request.Cookies[".AspNetCore.Meta.Metadata.role"].ToString();
            request = await _roasterRequestService.FetchSingleRoasterRequestByIdAsync(id);
            if (request == null)
                return BadRequest();
            return Page();
        }

        public async Task<IActionResult> OnPostProcessAsync()
        {
            try
            {
                request.Roaster.Picture = builders.BytePictureBuilder.GetBytePicture(Picture);
                var respCode = await _roasterRequestService.UpdateRoasterRequestAsync(request);
                if (respCode.Equals(0))
                    return RedirectToPage("RoasterRequests");
                else
                    return BadRequest();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}