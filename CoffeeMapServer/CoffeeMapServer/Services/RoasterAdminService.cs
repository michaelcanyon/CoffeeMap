using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeMapServer.builders;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using CoffeeMapServer.Services.Interfaces.Admin;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CoffeeMapServer.Services
{
    public class RoasterAdminService : IRoasterAdminService
    {
        private readonly IRoasterRepository _roasterRepository;
        private readonly IRoasterTagRepository _roasterTagRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IAddressRepository _addressReposiotry;
        private readonly ILogger<RoasterAdminService> _logger;

        public RoasterAdminService(IRoasterRepository roasterRepository,
                                   IRoasterTagRepository roasterTagRepository,
                                   ITagRepository tagRepository,
                                   IAddressRepository addressRepository,
                                   ILogger<RoasterAdminService> logger)
        {
            _roasterRepository = roasterRepository ?? throw new ArgumentNullException(nameof(roasterRepository));
            _roasterTagRepository = roasterTagRepository ?? throw new ArgumentNullException(nameof(roasterTagRepository));
            _tagRepository = tagRepository ?? throw new ArgumentNullException(nameof(tagRepository));
            _addressReposiotry = addressRepository ?? throw new ArgumentNullException(nameof(addressRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(addressRepository));
        }

        public async Task<int> AddRoasterAsync(Roaster roaster,
                                          string tags,
                                          Address address,
                                          IFormFile picture)
        {
            try
            {
                _logger.LogInformation("Roaster admin service layer access in progress...");

                var roasterByName = await _roasterRepository.GetRoasterByNameAsync(roaster.Name);
                if (roasterByName != null)
                    return -1;

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
                var _localTags = await RoasterAdminServiceBuilder.BuildTagsList(tags, _tagRepository);

                //process address entity
                var _address = Address.New(address.AddressStr, address.OpeningHours);
                roaster.OfficeAddress = _address;
                _addressReposiotry.Add(_address);
                //add roasterTags  notes
                RoasterTagsPairsBuilder.BuildRoasterTags(_localTags, roaster.Id, _roasterTagRepository);

                //process picture
                roaster.Picture = BytePictureBuilder.GetBytePicture(picture);
                _roasterRepository.Add(roaster);
                await _roasterRepository.SaveChangesAsync();

                _logger.LogInformation($"Roaster, Tags, RoasterTags, Addresses tables have been modified. Inserted roaster:\n Id: {roaster.Id}\n Roaster name: {roaster.Name}");
                return 0;
            }
            catch (Exception e)
            {
                _logger.LogError($"Roaster admin service layer error occured! Error text message: {e.Message}");
                return -2;
            }
        }

        public async Task<int> DeleteRoasterByIdAsync(Guid id)
        {
            try
            {
                _logger.LogInformation("Roaster admin service layer access in progress...");
                var roaster = await _roasterRepository.GetSingleAsync(id);
                _roasterRepository.Delete(roaster);
                var pairs = await _roasterTagRepository.GetPairsByRoasterIdAsync(id);
                _roasterTagRepository.DeleteRoasterTags(pairs);
                await _roasterRepository.SaveChangesAsync();
                _logger.LogInformation($"Roaster, Tags, RoasterTags, Addresses tables have been modified. Deleted roaster:\n Id: {roaster.Id}\n Roaster name: {roaster.Name}");
                return 0;
            }
            catch (Exception e)
            {
                _logger.LogError("Roaster admin service layer error occured! Error text message:" + e.Message);
                return -1;
            }
        }

        public async Task<IList<Roaster>> FetchRoastersAsync()
            => await _roasterRepository.GetListAsync();

        public async Task<IList<RoasterTag>> FetchRoasterTagsAsync(Guid roasterId)
            => await _roasterTagRepository.GetPairsByRoasterIdAsync(roasterId);

        public async Task<IList<RoasterTag>> FetchRoasterTagsAsync()
            => await _roasterTagRepository.GetListAsync();

        public async Task<IList<Tag>> FetchTagsAsync()
            => await _tagRepository.GetListAsync();

        public async Task<Tag> FetchTagByIdAsync(Guid id)
            => await _tagRepository.GetSingleAsync(id);

        public async Task<int> UpdateRoasterAsync(Roaster entity, string newTags, string deletableTags, IFormFile picture)
        {
            //fetch tags that already exists in current roaster note
            var onGetTags = new List<string>();
            onGetTags.AddRange(await RoasterAdminServiceBuilder.FetchRoasterTags(_roasterTagRepository,
                                                                                 _tagRepository,
                                                                                 entity.Id));

            //Add new tags.
            onGetTags.AddRange(await RoasterAdminServiceBuilder.BindTagsWithRoaster(newTags,
                                                                                    onGetTags,
                                                                                    _tagRepository,
                                                                                    _roasterTagRepository,
                                                                                    entity.Id));

            //Delete tags
            await RoasterAdminServiceBuilder.DeleteTags(onGetTags,
                                                        deletableTags,
                                                        _tagRepository,
                                                        _roasterTagRepository,
                                                        entity.Id);
            try
            {
                _logger.LogInformation("Roaster admin service layer access in progress...");
                //TODO: remove when declare meanings additions with fluent api
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

                entity.Picture = BytePictureBuilder.GetBytePicture(picture);
                var roasterAddress = await _addressReposiotry.GetSingleAsync(entity.OfficeAddress.Id);
                entity.OfficeAddress = roasterAddress;
                _roasterRepository.Update(entity);
                await _roasterTagRepository.SaveChangesAsync();
                _logger.LogInformation($"Roaster, Tags, RoasterTags, Addresses tables have been modified. Updated roaster:\n Id: {entity.Id}\n Roaster name: {entity.Name}");
                return 0;
            }
            catch (Exception e)
            {
                _logger.LogError("Roaster admin service layer error occured! Error text message:" + e.Message);
                return -1;
            }
        }

        public async Task<Roaster> FetchSingleRoasterAsync(Guid id)
            => await _roasterRepository.GetSingleAsync(id);
    }
}