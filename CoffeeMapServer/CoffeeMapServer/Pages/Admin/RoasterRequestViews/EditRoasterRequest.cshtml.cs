using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace CoffeeMapServer.Pages.Admin.RoasterRequestViews
{
    public class EditRoasterRequestModel : PageModel
    {
        private readonly IRoasterRequestRepository _roasterRequestRepository;
        [BindProperty]
       
        public RoasterRequest request { get; set; }
        
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
            await _roasterRequestRepository.Update(request);
            return RedirectToPage("RoasterRequests");
        }
    }
}
