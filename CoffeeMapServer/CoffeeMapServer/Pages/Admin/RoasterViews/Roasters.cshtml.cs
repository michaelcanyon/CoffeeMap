using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using CoffeeMapServer.Models.Intermediary_models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoffeeMapServer.Views.Admin.RoasterViews
{
    public class RoastersModel : PageModel
    {
        private readonly IRoasterRepository RoasterRepository;
        private readonly IRoasterTagRepository RoasterTagRepository;
        private readonly ITagRepository TagRepository;
        public List<Roaster> roasters { get; set; }
        public List<RoasterTag> roasterTags { get; set; }

        public List<Tag> tags { get; set; }

        [BindProperty(SupportsGet = true)]
        public string idFilter { get; set; }
        [BindProperty(SupportsGet = true)]
        public string nameFilter { get; set; }
        [BindProperty(SupportsGet = true)]
        public string officeAddressIdFilter { get; set; }
        [BindProperty(SupportsGet = true)]
        public string contactNumberFilter { get; set; }
        [BindProperty(SupportsGet = true)]
        public string contactEmailFilter { get; set; }
        [BindProperty(SupportsGet = true)]
        public string instagramProfileFilter { get; set; }
        [BindProperty(SupportsGet = true)]
        public string TelegramProfileFilter { get; set; }
        [BindProperty(SupportsGet = true)]
        public string VkProfileFilter { get; set; }
        [BindProperty(SupportsGet = true)]
        public string WebSiteFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string TagString { get; set; }
        public RoastersModel(IRoasterRepository roasterRepository, IRoasterTagRepository roasterTagRepository, ITagRepository tagRepository)
        {
            RoasterRepository = roasterRepository;
            RoasterTagRepository = roasterTagRepository;
            TagRepository = tagRepository;
        }

        [BindProperty]
        public int roasterId { get; set; }
        public string role { get; set; }
        public string nickname { get; set; }
        public async Task OnGetAsync()
        {
            nickname = HttpContext.Request.Cookies[".AspNetCore.Meta.Metadta.nickname"].ToString();
            role = HttpContext.Request.Cookies[".AspNetCore.Meta.Metadta.role"].ToString();
            roasters = await RoasterRepository.GetList();
            roasterTags = await RoasterTagRepository.GetList();
            tags = await TagRepository.GetList();
            if (!string.IsNullOrEmpty(idFilter))
                roasters = roasters.Where(s => s.Id.Equals(Convert.ToInt32(idFilter))).ToList();
            if (!string.IsNullOrEmpty(nameFilter))
                roasters = roasters.Where(s => s.Name.Contains(nameFilter)).ToList();
            if (!string.IsNullOrEmpty(officeAddressIdFilter))
                roasters = roasters.Where(s => s.OfficeAddressId.Equals(Convert.ToInt32(officeAddressIdFilter))).ToList();
            if (!string.IsNullOrEmpty(contactNumberFilter))
                roasters = roasters.Where(s => s.ContactNumber.Contains(contactNumberFilter)).ToList();
            if (!string.IsNullOrEmpty(contactEmailFilter))
                roasters = roasters.Where(s => s.ContactEmail.Contains(contactEmailFilter)).ToList();
            if (!string.IsNullOrEmpty(instagramProfileFilter))
                roasters = roasters.Where(s => s.InstagramProfileLink.Contains(instagramProfileFilter)).ToList();
            if (!string.IsNullOrEmpty(VkProfileFilter))
                roasters = roasters.Where(s => s.VkProfileLink.Contains(VkProfileFilter)).ToList();
            if (!string.IsNullOrEmpty(TelegramProfileFilter))
                roasters = roasters.Where(s => s.TelegramProfileLink.Contains(TelegramProfileFilter)).ToList();
            if (!string.IsNullOrEmpty(TagString))
            {
                var tagsArr = TagString.Split(" ");
                foreach (var i in tagsArr)
                {
                    if (i == "")
                        continue;
                    tags = tags.Where(tag => tag.TagTitle.Contains(i)).ToList();
                }
            }

        }

    }
}
