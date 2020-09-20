using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeMapServer.Infrastructures.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoffeeMapServer.Pages.Admin.RoasterRequestViews
{
    public class DeleteRoasterRequestModel : PageModel
    {
        private readonly IRoasterRequestRepository _roasterRequestRepository;
        public DeleteRoasterRequestModel(IRoasterRequestRepository roasterRequestRepository)
        {
            _roasterRequestRepository = roasterRequestRepository;
        }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            await _roasterRequestRepository.Delete(Convert.ToInt32(id));
            return RedirectToPage("RoasterRequets");
        }
    }
}
