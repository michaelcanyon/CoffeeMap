using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
        public int OfficeAddressId { get; set; }
        [Required]
        public string ContactNumber { get; set; }
        public string ContactEmail { get; set; }
        public string WebSiteLink { get; set; }
        public string VkProfileLink { get; set; }
        public string InstagramProfileLink { get; set; }
        public string TelegramProfileLink { get; set; }

        //public byte[] Picture { get; set; }
        //public string Description { get; set; }

    }
}
