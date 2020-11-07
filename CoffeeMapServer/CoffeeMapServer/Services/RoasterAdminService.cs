using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using CoffeeMapServer.Models.Intermediary_models;
using CoffeeMapServer.Services.Interfaces.Admin;
using Microsoft.AspNetCore.Http;

namespace CoffeeMapServer.Services
{
    public class RoasterAdminService : IRoasterAdminService
    {
        private readonly IRoasterRepository _roasterRepository;
        private readonly IRoasterTagRepository _roasterTagRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IAddressRepository _addressReposiotry;

        public RoasterAdminService(
            IRoasterRepository roasterRepository,
            IRoasterTagRepository roasterTagRepository,
            ITagRepository tagRepository,
            IAddressRepository addressRepository)
        {
            _roasterRepository = roasterRepository;
            _roasterTagRepository = roasterTagRepository;
            _tagRepository = tagRepository;
            _addressReposiotry = addressRepository;
        }

        public async Task AddRoasterAsync(Roaster roaster, string tags, Address address, IFormFile picture)
        {
            if (await _roasterRepository.GetRoasterByNameAsync(roaster.Name) != null)
                return;
            string[] tags_array;
            if (roaster.ContactEmail == null)
                roaster.ContactEmail = "none";
            if (roaster.InstagramProfileLink == null)
                roaster.InstagramProfileLink = "none";
            if (roaster.WebSiteLink == null)
                roaster.WebSiteLink = "none";
            if (roaster.VkProfileLink == null)
                roaster.VkProfileLink = "none";
            if (roaster.TelegramProfileLink == null)
                roaster.TelegramProfileLink = "none";
            if (address.OpeningHours == null)
                address.OpeningHours = "none";

            //Get and process tags string into tag entities
            var _localTags = new List<Tag>();
            if (tags.Length == 0)
            {
                tags = "none";
                tags_array = tags.Split("#");
            }
            else
            {
                tags_array = tags.Split("#");
                foreach (var i in tags_array)
                {
                    if (i == "")
                        continue;
                    Tag tempTag = await _tagRepository.GetSingleAsync(i);
                    if (tempTag is null)
                    {
                        var newTag = Tag.New(i);
                        _tagRepository.Add(newTag);
                        _localTags.Add(newTag);
                    }
                    else
                        _localTags.Add(tempTag);
                }
            }

            //process address entity
            var _address = Address.New(address.AddressStr, address.OpeningHours);
            _addressReposiotry.Add(_address);
            roaster.OfficeAddressId = _address.Id;

            //process picture
            if (picture != null)
            {
                byte[] bytePicture = null;
                using (var binaryReader = new BinaryReader(picture.OpenReadStream()))
                {
                    bytePicture = binaryReader.ReadBytes((int)picture.Length);
                }
                roaster.Picture = bytePicture;
            }
            _roasterRepository.Add(roaster);

            //add roasterTags  notes
            foreach (var i in _localTags)
                _roasterTagRepository.Add(new RoasterTag(roaster.Id, i.Id));

            await _roasterRepository.SaveChangesAsync();
            await _roasterTagRepository.SaveChangesAsync();
            await _tagRepository.SaveChangesAsync();
            await _addressReposiotry.SaveChangesAsync();
        }

        public async Task DeleteRoasterByIdAsync(Guid id)
        {
            var roaster = await _roasterRepository.GetSingleAsync(id);
            _roasterRepository.Delete(roaster);
            var pairs = await _roasterTagRepository.GetPairsByRoasterIdAsync(id);
            _roasterTagRepository.DeleteRange(pairs);
            await _roasterRepository.SaveChangesAsync();
            await _roasterTagRepository.SaveChangesAsync();
        }

        public async Task<IList<Roaster>> FetchRoastersAsync()
            => await _roasterRepository.GetListAsync();

        public async Task<IList<RoasterTag>> FetchRoasterTagsAsync(Guid roasterId)
            => await _roasterTagRepository.GetPairsByRoasterIdAsync(roasterId);

        public async Task<IList<Tag>> FetchTagsAsync()
            => await _tagRepository.GetListAsync();

        public async Task<Tag> FetchTagByIdAsync(Guid id)
            =>await _tagRepository.GetSingleAsync(id);

        public async Task UpdateRoasterAsync(Roaster entity, string newTags, string deletableTags, IFormFile picture)
        {
            //fetch tags that already exists in current roaster note
            var pairs = await _roasterTagRepository.GetPairsByRoasterIdAsync(entity.Id);
            var onGetTags = new List<string>();
            foreach (var item in pairs)
                onGetTags.Add((await _tagRepository.GetSingleAsync(item.TagId)).TagTitle);

            //Add new tags. Cicle is necessary for tags union
            if (newTags != null && newTags.Length > 0)
            {
                var addTagsList = newTags.Split("#");
                foreach (var i in addTagsList)
                    if (!onGetTags.Contains(i))
                    {
                        if (i == "")
                            continue;
                        Tag tempTag = await _tagRepository.GetSingleAsync(i);
                        if (tempTag is null)
                        {
                            tempTag = Tag.New(i);
                            _tagRepository.Add(tempTag);
                        }
                        _roasterTagRepository.Add(new RoasterTag(entity.Id, tempTag.Id));
                        onGetTags.Add(i);
                    }
            }

            //Delete tags
            if (deletableTags != null && deletableTags.Length > 0)
            {
                var delTags = deletableTags.Split("#");
                foreach (var item in delTags)
                    if (onGetTags.Contains(item))
                    {
                        if (item == "")
                            continue;
                        var tag = await _tagRepository.GetSingleAsync(item);
                        var roasterTag = pairs.FirstOrDefault(n => n.RoasterId == entity.Id && n.TagId == tag.Id);
                        _roasterTagRepository.Delete(roasterTag);
                        onGetTags.Remove(item);
                    }
            }

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

            if (picture != null)
            {
                byte[] bytePicture = null;
                using (var binaryReader = new BinaryReader(picture.OpenReadStream()))
                {
                    bytePicture = binaryReader.ReadBytes((int)picture.Length);
                }
                entity.Picture = bytePicture;
            }

            _roasterRepository.Update(entity);
            await _roasterRepository.SaveChangesAsync();
            await _tagRepository.SaveChangesAsync();
            await _roasterTagRepository.SaveChangesAsync();
        }

        public async Task<Roaster> FetchSingleRoasterAsync(Guid id)
            => await _roasterRepository.GetSingleAsync(id);
    }
}
