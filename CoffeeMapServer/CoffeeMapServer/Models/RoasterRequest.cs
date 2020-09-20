using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeMapServer.Models
{
    public class RoasterRequest:Entity
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
        //public byte[] Picture { get; set; }
        public string TagString { get; set; }
        public string Description { get; set; }
        [Required]
        public string AddressStr { get; set; }
        [Required]
        public string OpeningHours { get; set; }
    }
}
