using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoffeeMapServer.Pages.Admin.RoasterRequestViews
{
    public class RoasterRequestsModel : PageModel
    {
        private readonly IRoasterRequestRepository _roasterRequestRepository;

        public RoasterRequestsModel(IRoasterRequestRepository roasterRequestRepository)
        {
            _roasterRequestRepository = roasterRequestRepository;
        }

        [BindProperty]
        public List<RoasterRequest> RoasterRequests { get; set; }

        public string NDeletableRequests { get; set; }

        [BindProperty]
        public string RequestId { get; set; }

        [BindProperty]
        public Address Address { get; set; }

        [BindProperty]
        public Roaster Roaster { get; set; }

        public string Role { get; set; }

        public async Task OnGetAsync()
        {
            Role = HttpContext.Request.Cookies[".AspNetCore.Meta.Metadta.role"].ToString();
            RoasterRequests = await _roasterRequestRepository.GetList();
        }

        public async Task<IActionResult> OnPostDeleteAllAsync()
        {
            await _roasterRequestRepository.DeleteAll();
            return RedirectToPage("RoasterRequests");
        }
    }
}
