using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoffeeMapServer.Views.Admin
{
    public class AddRoasterModel : PageModel
    {
        private readonly IRoasterRepository roasterRepository;

        [BindProperty]
        public Roaster roaster { get; set; }
        public AddRoasterModel(IRoasterRepository repository) 
        {
            roasterRepository = repository;
        }
        public async Task<IActionResult> OnPostAsync()
        {
            await roasterRepository.Create(roaster);
            return RedirectToPage("Roasters");
        }
    }
}
