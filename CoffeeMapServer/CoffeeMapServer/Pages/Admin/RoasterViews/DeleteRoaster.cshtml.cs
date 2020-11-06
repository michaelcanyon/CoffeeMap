using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoffeeMapServer.Pages.Admin.RoasterViews
{
    public class DeleteRoasterModel : PageModel
    {
        private readonly IRoasterRepository _roasterRepository;

        public DeleteRoasterModel(IRoasterRepository roasterRepository)
        {
            _roasterRepository = roasterRepository;
        }

        public async Task<IActionResult> OnGet(Guid id)
        {
            var roaster = await _roasterRepository.GetSingle(id);
            await _roasterRepository.Delete(roaster);
            return RedirectToPage("Roasters");
        }
    }
}
