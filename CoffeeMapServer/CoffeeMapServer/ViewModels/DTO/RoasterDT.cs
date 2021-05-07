using System;
using CoffeeMapServer.Models;

namespace CoffeeMapServer.ViewModels.DTO
{
    public class RoasterDT : Entity
    {
        public string ContactPersonName { get; set; }

        public string ContactPersonNumber { get; set; }

        public string Name { get; set; }

        public string ContactNumber { get; set; }

        public string ContactEmail { get; set; }

        public string WebSiteLink { get; set; }

        public string VkProfileLink { get; set; }

        public string InstagramProfileLink { get; set; }

        public string TelegramProfileLink { get; set; }

        public byte[] Picture { get; set; }

        public string Description { get; set; }

        public RoasterDT() { }

        public RoasterDT(Guid? id = null) : base(id)
        { }

        public static RoasterDT New(Guid id,
                                  string contactPersonName,
                                  string contactPersonNumber,
                                  string name,
                                  string contactNumber,
                                  string contactEmail,
                                  string webSiteLink,
                                  string vkProfileLink,
                                  string igProfileLink,
                                  string tgProfileLink,
                                  byte[] picture,
                                  string description)
            => new RoasterDT(id)
            {
                ContactPersonName = contactPersonName,
                ContactPersonNumber = contactPersonNumber,
                Name = name,
                ContactNumber = contactNumber,
                ContactEmail = contactEmail,
                WebSiteLink = webSiteLink,
                VkProfileLink = vkProfileLink,
                InstagramProfileLink = igProfileLink,
                TelegramProfileLink = tgProfileLink,
                Picture = picture,
                Description = description
            };
    }
}
