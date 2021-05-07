using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CoffeeMapServer.Infrastructure.Interface;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using CoffeeMapServer.Models.OwnedModels;
using CoffeeMapServer.ViewModels.DTO;

namespace CoffeeMapServer.builders
{
    public static class RoasterRequestServiceBuilder
    {
        public static async Task<IList<Tag>> BuildAndBindTags(string tagString, ITagRepository tagRepository)
        {
            if (tagString == null || tagString == "")
                return null;
            var tags = tagString.Split("#");
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

        public static string BuildTagsString(IList<TagDT> tagsList)
        {
            if (tagsList.Count == 0)
                return "";
            StringBuilder tags = new StringBuilder();
            foreach (var i in tagsList)
                tags.Append(i.Name + "#");

            //Remove the last '#' symbol
            tags.Length--;
            return tags.ToString();
        }

        public static RoasterRequest GenerateRoasterRequest(RoasterRequestDT roasterRequestDT,
                                                            IPictureRequestRepository pictureRequestRepository)
        {
            var tags = BuildTagsString(roasterRequestDT.Tags);
            var roasterRequest = RoasterRequest.New(OwnedRoaster.New(roasterRequestDT.OwnerDT.Name + roasterRequestDT.OwnerDT.Surname,
                                                  roasterRequestDT.OwnerDT.PhoneNumber,
                                                  roasterRequestDT.RoasterDT.Name,
                                                  0,
                                                  roasterRequestDT.RoasterDT.ContactNumber,
                                                  roasterRequestDT.RoasterDT.ContactEmail,
                                                  roasterRequestDT.RoasterDT.WebSiteLink,
                                                  roasterRequestDT.RoasterDT.VkProfileLink,
                                                  roasterRequestDT.RoasterDT.InstagramProfileLink,
                                                  roasterRequestDT.RoasterDT.TelegramProfileLink,
                                                  roasterRequestDT.RoasterDT.Description,
                                                  DateTime.Now),
                                      OwnedAddress.New(roasterRequestDT.AddressDT.AddressStr,
                                                  roasterRequestDT.AddressDT.OpeningHours,
                                                  roasterRequestDT.AddressDT.Latitude,
                                                  roasterRequestDT.AddressDT.Longitude),
                                      tags);
            byte[] picture;

            try
            {
                 picture = roasterRequestDT.CharPicture.Length > 0 ? Convert.FromBase64String(roasterRequestDT.CharPicture.Substring(roasterRequestDT.CharPicture.LastIndexOf(',') + 1)) : new byte[0];
            }
            catch {
                picture = new byte[0];
            }

            BytePictureBuilder.BindPictureRequest(roasterRequest.Id,
                                                  picture,
                                                  pictureRequestRepository);
            return roasterRequest;
        }

        /// <summary>
        /// Generates Roaster entity brand new id with
        /// </summary>
        /// <param name="roaster"></param>
        /// <param name="addressId"></param>
        /// <returns>Roaster note</returns>
        public static Roaster GenerateRoaster(OwnedRoaster roasterRequest)
            => Roaster.New(roasterRequest.ContactPersonName,
                           roasterRequest.ContactPersonNumber,
                           roasterRequest.Name,
                           roasterRequest.ContactNumber,
                           roasterRequest.ContactEmail,
                           roasterRequest.WebSiteLink,
                           roasterRequest.VkProfileLink,
                           roasterRequest.InstagramProfileLink,
                           roasterRequest.TelegramProfileLink,
                           roasterRequest.Description,
                           DateTime.Now,
                           roasterRequest.Priority);
    }
}