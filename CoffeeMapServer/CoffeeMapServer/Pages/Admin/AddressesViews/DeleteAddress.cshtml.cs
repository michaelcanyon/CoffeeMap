using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeMapServer.Infrastructures.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoffeeMapServer.Views.Admin.Addresses
{
    public class DeleteAddressModel : PageModel
    {
        private readonly IAddessRepository addessRepository;
        private readonly IRoasterRepository roasterRepository;
        public DeleteAddressModel(IAddessRepository repository, IRoasterRepository rRepository)
        {
            addessRepository = repository;
            roasterRepository = rRepository;
        }
        public async Task<IActionResult> OnGet(int? id)
        {
            await addessRepository.Delete(Convert.ToInt32(id));
           var roaster= await roasterRepository.GetSingleByAddressId(Convert.ToInt32(id));
            roaster.OfficeAddressId = 900000000;
            await roasterRepository.Update(roaster);
            return RedirectToPage("GetAddresses");
        }
    }
}
