using System;
using System.ComponentModel.DataAnnotations;

namespace CoffeeMapServer.Models
{
    public class RoasterRequest : Entity
    {
        /// <summary>
        /// Roaster Name
        /// </summary>
        [Required]
        public string Name { get; set; }

        [Required]
        public string ContactNumber { get; set; }

        [Required]
        public string ContactEmail { get; set; }

        public string WebSiteLink { get; set; }

        public string VkProfileLink { get; set; }

        public string InstagramProfileLink { get; set; }

        public string TelegramProfileLink { get; set; }

        public byte[] Picture { get; set; }
        public string TagString { get; set; }

        public string Description { get; set; }

        [Required]
        public string AddressStr { get; set; }

        [Required]
        public string OpeningHours { get; set; }

        public RoasterRequest(Guid? id = null) : base(id)
        { }

        public static RoasterRequest New(Guid id, string name, string contactNumber, string contactEmail, string webSiteLink, string vkProfileLink, string igProfileLink, string tgProfileLink) => new RoasterRequest(id)
        {
            Name = name,
            ContactNumber = contactNumber,
            ContactEmail = contactEmail,
            WebSiteLink = webSiteLink,
            VkProfileLink = vkProfileLink,
            InstagramProfileLink = igProfileLink,
            TelegramProfileLink = tgProfileLink
        };

        public static RoasterRequest New(string name, string contactNumber, string contactEmail, string webSiteLink, string vkProfileLink, string igProfileLink, string tgProfileLink) => new RoasterRequest
        {
            Name = name,
            ContactNumber = contactNumber,
            ContactEmail = contactEmail,
            WebSiteLink = webSiteLink,
            VkProfileLink = vkProfileLink,
            InstagramProfileLink = igProfileLink,
            TelegramProfileLink = tgProfileLink
        };
    }
}
