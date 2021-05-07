using System;
using System.Collections.Generic;

namespace CoffeeMapServer.Models
{
    //TODO: Add priority and registration date and time fields
    public class Roaster : Entity
    {
        public string Name { get; set; }

        public Address OfficeAddress { get; set; }

        public string ContactPersonName { get; set; }

        public string ContactPersonPhone { get; set; }

        public string ContactNumber { get; set; }

        public string ContactEmail { get; set; }

        public string WebSiteLink { get; set; }

        public string VkProfileLink { get; set; }

        public string InstagramProfileLink { get; set; }

        public string TelegramProfileLink { get; set; }

        public Guid PictureId { get; set; }

        public Picture Picture { get; set; }

        public string Description { get; set; }

        public DateTime CreationDate { get; set; }
        
        public int Priority { get; set; }

        public ICollection<RoasterTag> RoasterTags { get; set; }

        public Roaster() { }

        public Roaster(Guid? id = null) : base(id)
        { }

        public static Roaster New(Guid id,
                                  string contactPersonName,
                                  string contactPersonPhone,
                                  string name,
                                  string contactNumber,
                                  string contactEmail,
                                  string webSiteLink,
                                  string vkProfileLink,
                                  string igProfileLink,
                                  string tgProfileLink,
                                  string description,
                                  DateTime creationDate,
                                  int priority)
            => new Roaster(id)
            {
                ContactPersonName=contactPersonName,
                ContactPersonPhone=contactPersonPhone,
                Name = name,
                ContactNumber = contactNumber,
                ContactEmail = contactEmail,
                WebSiteLink = webSiteLink,
                VkProfileLink = vkProfileLink,
                InstagramProfileLink = igProfileLink,
                TelegramProfileLink = tgProfileLink,
                Description = description,
                CreationDate=creationDate,
                Priority=priority
            };

        public static Roaster New(string contactPersonName,
                                  string contactPersonPhone,
                                  string name,
                                  string contactNumber,
                                  string contactEmail,
                                  string webSiteLink,
                                  string vkProfileLink,
                                  string igProfileLink,
                                  string tgProfileLink,
                                  string description,
                                  DateTime creationDate,
                                  int priority)
            => new Roaster()
            {
                ContactPersonName=contactPersonName,
                ContactPersonPhone=contactPersonPhone,
                Name = name,
                ContactNumber = contactNumber,
                ContactEmail = contactEmail,
                WebSiteLink = webSiteLink,
                VkProfileLink = vkProfileLink,
                InstagramProfileLink = igProfileLink,
                TelegramProfileLink = tgProfileLink,
                Description = description,
                CreationDate=creationDate,
                Priority=priority
            };

    }
}