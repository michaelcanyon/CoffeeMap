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
        private readonly IRoasterRepository roasterRepository;

        public Guid Guid { get; set; }

        public DeleteRoasterModel(IRoasterRepository repository)
        {
            roasterRepository = repository;
        }

        public async Task<IActionResult> OnGet(Guid id)
        {
            Guid = id;
            await roasterRepository.Delete(Guid);
            return RedirectToPage("Roasters");
        }
    }
}
