using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using CoffeeMapServer.Services.Interfaces.Admin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CoffeeMapServer.Pages.Admin.RoasterRequestViews
{
    public class EditRoasterRequestModel : PageModel
    {
        private readonly IRoasterRequestService _roasterRequestService;
        [BindProperty]
       
        public RoasterRequest request { get; set; }
        
        [BindProperty]
        public IFormFile Picture { get; set; }

        public Guid Guid { get; set; }
        
        public string Role { get; set; }
        
        public EditRoasterRequestModel(IRoasterRequestService roasterRequestService)
            =>_roasterRequestService = roasterRequestService;
        
        public async Task OnGetAsync(Guid id)
        {
            Guid = id;
            Role = HttpContext.Request.Cookies[".AspNetCore.Meta.Metadta.role"].ToString();
            request = await _roasterRequestService.FetchSingleRoasterRequestByIdAsync(Guid);
        }
        
        public async Task<IActionResult> OnPostProcessAsync()
        {
            if (Picture != null)
            {
                byte[] bytePicture = null;
                using (var binaryReader = new BinaryReader(Picture.OpenReadStream()))
                {
                    bytePicture = binaryReader.ReadBytes((int)Picture.Length);
                }
                request.Picture = bytePicture;
            }
            await _roasterRequestService.UpdateRoasterRequestAsync(request);
            return RedirectToPage("RoasterRequests");
        }
    }
}
