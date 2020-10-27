using CoffeeMapServer.Infrastructures.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace CoffeeMapServer.Views.Admin.Addresses
{
    public class DeleteAddressModel : PageModel
    {
        private readonly IAddessRepository addessRepository;
        private readonly IRoasterRepository roasterRepository;

        public Guid Guid { get; set; }

        public DeleteAddressModel(IAddessRepository repository, IRoasterRepository rRepository)
        {
            addessRepository = repository;
            roasterRepository = rRepository;
        }

        public async Task<IActionResult> OnGet(Guid id)
        {
            // TODO: обсуди с женей. Возможно, необходимо удалить все адреса у оффисов, где они совпадают. Ну или сам подумай.
            Guid = id;
            await addessRepository.Delete(id);
            var roaster = await roasterRepository.GetSingleByAddressId(id);
            if (roaster != null)
            {
                roaster.OfficeAddressId = new Guid();
                await roasterRepository.Update(roaster);
            }
            return RedirectToPage("GetAddresses");
        }
    }
}
