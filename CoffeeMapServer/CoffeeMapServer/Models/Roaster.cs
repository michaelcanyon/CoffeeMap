using System;
using System.ComponentModel.DataAnnotations;

namespace CoffeeMapServer.Models
{
    public class Roaster : Entity
    {
        /// <summary>
        /// Roaster Name
        /// </summary>
        [Required]
        public string Name { get; set; }
     
        [Required]
        public Guid OfficeAddressId { get; set; }
        
        [Required]
        public string ContactNumber { get; set; }
        
        public string ContactEmail { get; set; }
        
        public string WebSiteLink { get; set; }
        
        public string VkProfileLink { get; set; }
        
        public string InstagramProfileLink { get; set; }
        
        public string TelegramProfileLink { get; set; }

        public byte[] Picture { get; set; }
        public string Description { get; set; }
        //public Roaster() : base()
        //{ }

    }
}
