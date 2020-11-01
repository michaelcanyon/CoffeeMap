using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
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
        private readonly IRoasterRequestRepository _roasterRequestRepository;
        [BindProperty]
       
        public RoasterRequest request { get; set; }
        
        [BindProperty]
        public IFormFile Picture { get; set; }

        public Guid Guid { get; set; }
        
        public string Role { get; set; }
        
        public EditRoasterRequestModel(IRoasterRequestRepository roasterRequestRepository)
        {
            _roasterRequestRepository = roasterRequestRepository;
        }
        
        public async Task OnGetAsync(Guid id)
        {
            Guid = id;
            Role = HttpContext.Request.Cookies[".AspNetCore.Meta.Metadta.role"].ToString();
            request = await _roasterRequestRepository.GetSingle(Guid);
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
            await _roasterRequestRepository.Update(request);
            return RedirectToPage("RoasterRequests");
        }
    }
}
