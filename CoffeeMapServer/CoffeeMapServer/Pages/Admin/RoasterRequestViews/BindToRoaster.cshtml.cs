using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using CoffeeMapServer.Models.Intermediary_models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace CoffeeMapServer.Pages.Admin.RoasterRequestViews
{
    public class BindToRoasterModel : PageModel
    {
        private readonly IRoasterRequestRepository _roasterRequestRepository;
        private readonly IAddessRepository _addressRepository;
        private readonly IRoasterTagRepository _roasterTagRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IRoasterRepository _roasterRepository;
        private List<Tag> bindTags { get; set; }
        private Address address { get; set; }
        private Roaster roaster { get; set; }
        public BindToRoasterModel(IRoasterRequestRepository roasterRequestRepository,IRoasterRepository roasterRepository,
            IRoasterTagRepository roasterTagRepository, ITagRepository tagRepository, IAddessRepository addressRepository)
        {
            _roasterRequestRepository = roasterRequestRepository;
            _addressRepository = addressRepository;
            _roasterRepository = roasterRepository;
            _roasterTagRepository = roasterTagRepository;
            _tagRepository = tagRepository;
        }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var request= await _roasterRequestRepository.GetSingle(Convert.ToInt32(id));
            var tags = request.TagString.Split(" ");
            bindTags = new List<Tag>();
            foreach (var item in tags)
            { 
                if (await _tagRepository.GetSingle(item)== null)
                    await _tagRepository.Create(new Tag { TagTitle = item });
                bindTags.Add(await _tagRepository.GetSingle(item));
            }
            address = new Address();
            address.AddressStr = request.AddressStr;
            address.OpeningHours = request.OpeningHours;
               await _addressRepository.Create(address);
            address = await _addressRepository.GetSingle(address);
            if (address == null)
                return RedirectToPage("RoasterRequests");
            roaster = new Roaster();
            roaster.OfficeAddressId = address.Id;
            roaster.Name = request.Name;
            roaster.ContactNumber = request.ContactNumber;
            roaster.ContactEmail = request.ContactEmail;
            roaster.WebSiteLink = request.WebSiteLink;
            roaster.Description = request.Description;
            roaster.TelegramProfileLink = request.TelegramProfileLink;
            roaster.InstagramProfileLink = request.InstagramProfileLink;
            roaster.VkProfileLink = request.VkProfileLink;
            await _roasterRepository.Create(roaster);
            var addedRoaster = await _roasterRepository.GetRoaster(roaster);
            foreach (var item in bindTags)
            {
               await _roasterTagRepository.Create(new RoasterTag { RoasterId = addedRoaster.Id, TagId = item.Id });
            }
            await _roasterRequestRepository.Delete(Convert.ToInt32(id));
            return RedirectToPage("RoasterRequests");
        }
    }
}
