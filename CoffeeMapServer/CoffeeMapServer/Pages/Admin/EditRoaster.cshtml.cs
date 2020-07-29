using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Infrastructures.Repositories;
using CoffeeMapServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoffeeMapServer.Views.Admin
{
    public class EditRoasterModel : PageModel
    {
        private readonly IRoasterRepository roasterRepository;

        [BindProperty]
        public Roaster roaster { get; set; }
        public EditRoasterModel(IRoasterRepository repository)
        {
            roasterRepository = repository;
        }
        public async Task<IActionResult> OnGet(int? id)
        {
           roaster = await roasterRepository.GetSingle(Convert.ToInt32(id));
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
           await roasterRepository.Update(roaster);
            return RedirectToPage("Roasters");
        }
    }
}
