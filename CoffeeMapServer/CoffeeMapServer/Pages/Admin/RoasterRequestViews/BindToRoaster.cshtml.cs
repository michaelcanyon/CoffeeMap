using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using CoffeeMapServer.Models.Intermediary_models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoffeeMapServer.Pages.Admin.RoasterRequestViews
{
    public class BindToRoasterModel : PageModel
    {
        private readonly IRoasterRequestRepository _roasterRequestRepository;
        private readonly IAddessRepository _addressRepository;
        private readonly IRoasterTagRepository _roasterTagRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IRoasterRepository _roasterRepository;

        public Guid Guid { get; set; }
        
        private List<Tag> BindTags { get; set; }
        
        private Address Address { get; set; }
        
        private Roaster Roaster { get; set; }
        
        public BindToRoasterModel(IRoasterRequestRepository roasterRequestRepository, IRoasterRepository roasterRepository,
            IRoasterTagRepository roasterTagRepository, ITagRepository tagRepository, IAddessRepository addressRepository)
        {
            _roasterRequestRepository = roasterRequestRepository;
            _addressRepository = addressRepository;
            _roasterRepository = roasterRepository;
            _roasterTagRepository = roasterTagRepository;
            _tagRepository = tagRepository;
        }
        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var request = await _roasterRequestRepository.GetSingle(id);
            var tags = request.TagString.Split(";");
            BindTags = new List<Tag>();
            foreach (var item in tags)
            {
                if (await _tagRepository.GetSingle(item) == null)
                    await _tagRepository.Create(new Tag { TagTitle = item });
                BindTags.Add(await _tagRepository.GetSingle(item));
            }
            Address = new Address();
            Address.AddressStr = request.AddressStr;
            Address.OpeningHours = request.OpeningHours;
            await _addressRepository.Create(Address);
            Address = await _addressRepository.GetSingle(Address);
            if (Address == null)
                return RedirectToPage("RoasterRequests");
            Roaster = new Roaster();
            Roaster.OfficeAddressId = Address.Id;
            Roaster.Name = request.Name;
            Roaster.ContactNumber = request.ContactNumber;
            Roaster.ContactEmail = request.ContactEmail;
            Roaster.WebSiteLink = request.WebSiteLink;
            Roaster.Description = request.Description;
            Roaster.TelegramProfileLink = request.TelegramProfileLink;
            Roaster.InstagramProfileLink = request.InstagramProfileLink;
            Roaster.VkProfileLink = request.VkProfileLink;
            Roaster.Picture = request.Picture;
            await _roasterRepository.Create(Roaster);
            var addedRoaster = await _roasterRepository.GetRoaster(Roaster);
            foreach (var item in BindTags)
            {
                await _roasterTagRepository.Create(new RoasterTag { RoasterId = addedRoaster.Id, TagId = item.Id });
            }
            await _roasterRequestRepository.Delete(id);
            return RedirectToPage("RoasterRequests");
        }
    }
}
