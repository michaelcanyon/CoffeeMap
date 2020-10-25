using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoffeeMapServer.Pages.Admin.RoasterRequestViews
{
    public class EditRoasterRequestModel : PageModel
    {
        private readonly IRoasterRequestRepository _roasterRequestRepository;
        [BindProperty]
        public RoasterRequest request { get; set; }
        public Guid guid { get; set; }
        public string role { get; set; }
        public EditRoasterRequestModel(IRoasterRequestRepository roasterRequestRepository)
        {
            _roasterRequestRepository = roasterRequestRepository;
        }
        public async Task OnGetAsync(Guid id)
        {
            guid = id;
            role = HttpContext.Request.Cookies[".AspNetCore.Meta.Metadta.role"].ToString();
            request = await _roasterRequestRepository.GetSingle(guid);
        }
        public async Task<IActionResult> OnPostProcessAsync()
        {
            await _roasterRequestRepository.Update(request);
            return RedirectToPage("RoasterRequests");
        }
    }
}
