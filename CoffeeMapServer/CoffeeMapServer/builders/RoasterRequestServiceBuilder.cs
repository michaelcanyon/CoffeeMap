using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;

namespace CoffeeMapServer.builders
{
    public static class RoasterRequestServiceBuilder
    {
        public static async Task<IList<Tag>> BuildAndBindTags(string tagString, ITagRepository tagRepository)
        {
            var tags = tagString.Split(";");
            var bindTags = new List<Tag>();
            foreach (var item in tags)
            {
                //condition made to avoid excessive tag getting function call
                if (await tagRepository.GetSingleAsync(item) == null)
                {
                    var tag = Tag.New(item);
                    tagRepository.Add(tag);
                    bindTags.Add(tag);
                    continue;
                }
                bindTags.Add(await tagRepository.GetSingleAsync(item));
            }
            return bindTags;
        }

        /// <summary>
        /// Generates Roaster entity brand new id with
        /// </summary>
        /// <param name="roaster"></param>
        /// <param name="addressId"></param>
        /// <returns>Roaster note</returns>
        public static Roaster GenerateRoaster(Roaster roaster,
                                              Guid addressId)
            => Roaster.New(roaster.Name,
                           roaster.ContactNumber,
                           roaster.ContactEmail,
                           roaster.WebSiteLink,
                           roaster.VkProfileLink,
                           roaster.InstagramProfileLink,
                           roaster.TelegramProfileLink,
                           roaster.Picture,
                           roaster.Description);
    }
}