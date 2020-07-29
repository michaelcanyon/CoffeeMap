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
    public class RoastersModel : PageModel
    {
        private readonly IRoasterRepository RoasterRepository;
        public List<Roaster> roasters { get; set; }
        public RoastersModel(IRoasterRepository roasterRepository)
        {
            RoasterRepository = roasterRepository;
        }

        [BindProperty]
        public int roasterId { get; set; }
        public async Task OnGetAsync()
        {
            roasters = await RoasterRepository.GetList();
        }
        public IActionResult OnPostCreate()
        {
           return RedirectToPage("");
        }

    }
}
