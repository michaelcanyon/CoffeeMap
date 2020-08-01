using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoffeeMapServer.Views.Admin.RoasterViews
{
    public class AddRoasterModel : PageModel
    {
        private readonly IRoasterRepository roasterRepository;
        private readonly IAddessRepository addessRepository;

        [BindProperty]
        public Roaster roaster { get; set; }

        [BindProperty]
        public Address address { get; set; }
        public AddRoasterModel(IRoasterRepository repository, IAddessRepository addrRepository) 
        {
            roasterRepository = repository;
            addessRepository = addrRepository;

        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (roaster.ContactEmail == null)
                roaster.ContactEmail = "none";
            if(roaster.InstagramProfileLink==null)
                roaster.InstagramProfileLink = "none";
            if (roaster.WebSiteLink == null)
                roaster.WebSiteLink = "none";
            if (roaster.VkProfileLink == null)
                roaster.VkProfileLink = "none";
            if (roaster.TelegramProfileLink == null)
                roaster.TelegramProfileLink = "none";
            if (address.OpeningHours == null)
                address.OpeningHours = "none";

            await addessRepository.Create(address);
            var addr = await addessRepository.GetSingle(address);
            roaster.OfficeAddressId = addr.Id;
            await roasterRepository.Create(roaster);
            return RedirectToPage("Roasters");

        }
    }
}
