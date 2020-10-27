using CoffeeMapServer.Infrastructures.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace CoffeeMapServer.Pages.Admin.RoasterRequestViews
{
    public class DeleteRoasterRequestModel : PageModel
    {
        private readonly IRoasterRequestRepository _roasterRequestRepository;
        
        public Guid guid { get; set; }
        
        public DeleteRoasterRequestModel(IRoasterRequestRepository roasterRequestRepository)
        {
            _roasterRequestRepository = roasterRequestRepository;
        }
        
        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            guid = id;
            await _roasterRequestRepository.Delete(guid);
            return RedirectToPage("RoasterRequests");
        }
    }
}
