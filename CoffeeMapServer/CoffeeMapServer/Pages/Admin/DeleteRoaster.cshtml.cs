using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoffeeMapServer.Pages.Admin
{
    public class DeleteRoasterModel : PageModel
    {
        private readonly IRoasterRepository roasterRepository;
        public DeleteRoasterModel(IRoasterRepository repository)
        {
            roasterRepository = repository;
        }
        public async Task<IActionResult> OnGet(int? id)
        {
            await roasterRepository.Delete(Convert.ToInt32(id));
            return RedirectToPage("Roasters");
        }
    }
}
