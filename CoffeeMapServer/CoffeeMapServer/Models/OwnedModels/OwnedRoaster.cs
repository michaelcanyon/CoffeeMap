using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeMapServer.Models.OwnedModels
{
    public class OwnedRoaster
    {
        public string ContactPersonName { get; set; }

        public string ContactPersonNumber { get; set; }

        public string Name { get; set; }

        public int Priority { get; set; }

        public string ContactNumber { get; set; }

        public string ContactEmail { get; set; }

        public string WebSiteLink { get; set; }

        public string VkProfileLink { get; set; }

        public string InstagramProfileLink { get; set; }

        public string TelegramProfileLink { get; set; }

        public string Description { get; set; }

        public DateTime CreationDate{get;set;}

        public OwnedRoaster() { }


        public static OwnedRoaster New(string contactPersonName,
                                  string contactPersonNumber,
                                  string name,
                                  int priority,
                                  string contactNumber,
                                  string contactEmail,
                                  string webSiteLink,
                                  string vkProfileLink,
                                  string igProfileLink,
                                  string tgProfileLink,
                                  string description,
                                  DateTime creationDate)
            => new OwnedRoaster()
            {
                ContactPersonName = contactPersonName,
                ContactPersonNumber = contactPersonNumber,
                Name = name,
                Priority=priority,
                ContactNumber = contactNumber,
                ContactEmail = contactEmail,
                WebSiteLink = webSiteLink,
                VkProfileLink = vkProfileLink,
                InstagramProfileLink = igProfileLink,
                TelegramProfileLink = tgProfileLink,
                Description = description,
                CreationDate= creationDate
            };
    }
}
