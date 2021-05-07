using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;

namespace CoffeeMapServer.builders
{
    public static class RoasterAdminServiceBuilder
    {

        public static async Task UpdateRoasterTagsAsync(string tags,
                                                                    ITagRepository tagRepository,
                                                                    IRoasterTagRepository roasterTagRepository,
                                                                    Guid roasterId)
        {
            //TODO: check if it's work
            var tagsList = await BuildTagsListAsync(tags, tagRepository);
            var tagsIds = (await roasterTagRepository.GetPairsByRoasterIdAsNoTrackingAsync(roasterId)).Select(p => p.TagId);
            var tagsToAdd = tagsList.Where(t => !tagsIds.Contains(t.Id)).ToList();
            var tagsToDelete = tagsIds.Except(tagsList.Select(t => t.Id).ToList()).ToList();
            foreach (var i in tagsToAdd)
                roasterTagRepository.Add(new RoasterTag(roasterId, i.Id));
            foreach (var i in tagsToDelete)
                roasterTagRepository.Delete(new RoasterTag(roasterId, i));
        }

        public static async Task<IList<Tag>> BuildTagsListAsync(string tagsString, ITagRepository tagRepository)
        {
            List<string> tags_list;
            var _localTags = new List<Tag>();
            if (String.IsNullOrEmpty(tagsString))
                return _localTags;
            else
            {
                tags_list = tagsString.ToLower()
                                      .Split("#")
                                      .Distinct()
                                      .ToList();

                foreach (var i in tags_list)
                {
                    if (i == "")
                        continue;
                    Tag tempTag = await tagRepository.GetSingleAsNoTrackingAsync(i);
                    if (tempTag is null)
                    {
                        var newTag = Tag.New(i);
                        tagRepository.Add(newTag);
                        _localTags.Add(newTag);
                    }
                    else
                        _localTags.Add(tempTag);
                }
            }
            return _localTags;
        }

        public static Roaster AddRoasterNullPlugs(Roaster entity)
        {
            if (entity.ContactPersonName == null)
                entity.ContactPersonName = "none";
            if (entity.ContactPersonPhone == null)
                entity.ContactPersonPhone = "none";
            if (entity.ContactEmail == null)
                entity.ContactEmail = "none";
            if (entity.InstagramProfileLink == null)
                entity.InstagramProfileLink = "none";
            if (entity.WebSiteLink == null)
                entity.WebSiteLink = "none";
            if (entity.VkProfileLink == null)
                entity.VkProfileLink = "none";
            if (entity.TelegramProfileLink == null)
                entity.TelegramProfileLink = "none";

            return entity;
        }

        public static Address UpdateRoasterAddress(Address old, Address newOne)
        {
            old.AddressStr = newOne.AddressStr;
            old.OpeningHours = newOne.OpeningHours;
            old.Latitude = newOne.Latitude;
            old.Longitude = newOne.Longitude;
            if (old.OpeningHours == null)
                old.OpeningHours = "none";
            return old;
        }
    }
}