using System;
using System.Collections.Generic;

namespace CoffeeMapServer.Models
{
    public class Roaster : Entity
    {
        public string Name { get; set; }

        public Address OfficeAddress { get; set; }

        //Remove this shit
        //public Guid OfficeAddressId { get; set; }

        public string ContactNumber { get; set; }

        public string ContactEmail { get; set; }

        public string WebSiteLink { get; set; }

        public string VkProfileLink { get; set; }

        public string InstagramProfileLink { get; set; }

        public string TelegramProfileLink { get; set; }

        public byte[] Picture { get; set; }

        public string Description { get; set; }

        public ICollection<RoasterTag> RoasterTags { get; set; }

        public Roaster() { }

        public Roaster(Guid? id = null) : base(id)
        { }

        public static Roaster New(Guid id,
                                  string name,
                                  string contactNumber,
                                  string contactEmail,
                                  string webSiteLink,
                                  string vkProfileLink,
                                  string igProfileLink,
                                  string tgProfileLink,
                                  byte[] picture,
                                  string description)
            => new Roaster(id)
            {
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

        public static Roaster New(string name,
                                  string contactNumber,
                                  string contactEmail,
                                  string webSiteLink,
                                  string vkProfileLink,
                                  string igProfileLink,
                                  string tgProfileLink,
                                  byte[] picture,
                                  string description)
            => new Roaster()
            {
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