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
        public static async Task<IList<string>> FetchRoasterTags(IRoasterTagRepository roasterTagRepository,
                                                                  ITagRepository tagRepository,
                                                                  Guid roasterId)
        {
            var pairs = await roasterTagRepository.GetPairsByRoasterIdAsync(roasterId);
            var onGetTags = new List<string>();
            foreach (var item in pairs)
                onGetTags.Add((await tagRepository.GetSingleAsync(item.TagId)).TagTitle);
            return onGetTags;
        }

        public static async Task<IList<string>> BindTagsWithRoaster(string newTags,
                                                                    IList<string> currentTags,
                                                                    ITagRepository tagRepository,
                                                                    IRoasterTagRepository roasterTagRepository,
                                                                    Guid roasterId)
        {
            var newAddedTags = new List<string>();
            if (newTags != null && newTags.Length > 0)
            {
                var addTagsList = newTags.Split("#");
                foreach (var i in addTagsList)
                    if (!currentTags.Contains(i))
                    {
                        if (i == "")
                            continue;
                        Tag tempTag = await tagRepository.GetSingleAsync(i);
                        if (tempTag is null)
                        {
                            tempTag = Tag.New(i);
                            tagRepository.Add(tempTag);
                        }
                        roasterTagRepository.Add(new RoasterTag(roasterId, tempTag.Id));
                        newAddedTags.Add(i);
                    }
            }
            return newAddedTags;
        }

        public static async Task DeleteTags(IList<string> currentTags, string tagsToDelete, ITagRepository tagRepository, IRoasterTagRepository roasterTagRepository, Guid roasterId)
        {
            if (tagsToDelete != null && tagsToDelete.Length > 0)
            {
                var pairs = await roasterTagRepository.GetPairsByRoasterIdAsync(roasterId);
                var delTags = tagsToDelete.Split("#");
                foreach (var item in delTags)
                    if (currentTags.Contains(item))
                    {
                        if (item == "")
                            continue;
                        var tag = await tagRepository.GetSingleAsync(item);
                        var roasterTag = pairs.FirstOrDefault(n => n.TagId == tag.Id);
                        roasterTagRepository.Delete(roasterTag);
                        currentTags.Remove(item);
                    }
            }
        }

        public static async Task<IList<Tag>> BuildTagsList(string tagsString, ITagRepository tagRepository)
        {
            string[] tags_array;
            var _localTags = new List<Tag>();
            if (tagsString.Length == 0)
            {
                tagsString = "none";
                tags_array = tagsString.Split("#");
            }
            else
            {
                tags_array = tagsString.Split("#");
                foreach (var i in tags_array)
                {
                    if (i == "")
                        continue;
                    Tag tempTag = await tagRepository.GetSingleAsync(i);
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
    }
}