using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Infrastructures.Repositories;
using CoffeeMapServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoffeeMapServer.Views.Admin.RoasterViews
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
            if (roaster.ContactEmail == "none")
                roaster.ContactEmail = null;
            if (roaster.InstagramProfileLink == "none")
                roaster.InstagramProfileLink = null;
            if (roaster.TelegramProfileLink == "none")
                roaster.TelegramProfileLink = null;
            if (roaster.VkProfileLink == "none")
                roaster.VkProfileLink = null;
            if (roaster.WebSiteLink == "none")
                roaster.WebSiteLink = null;
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (roaster.ContactEmail == null)
                roaster.ContactEmail = "none";
            if (roaster.InstagramProfileLink == null)
                roaster.InstagramProfileLink = "none";
            if (roaster.WebSiteLink == null)
                roaster.WebSiteLink = "none";
            if (roaster.VkProfileLink == null)
                roaster.VkProfileLink = "none";
            if (roaster.TelegramProfileLink == null)
                roaster.TelegramProfileLink = "none";
            await roasterRepository.Update(roaster);
            return RedirectToPage("Roasters");
        }
    }
}
