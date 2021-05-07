using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeMapServer.builders;
using CoffeeMapServer.Infrastructure.Interface;
using CoffeeMapServer.Infrastructure.Repository;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using CoffeeMapServer.Services.Interfaces.Admin;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace CoffeeMapServer.Services
{
    public class RoasterAdminService : IRoasterAdminService
    {
        private readonly IRoasterRepository _roasterRepository;
        private readonly IRoasterTagRepository _roasterTagRepository;
        private readonly IPictureRepository _pictureRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IAddressRepository _addressReposiotry;
        private readonly ILogger _logger;

        public RoasterAdminService(IRoasterRepository roasterRepository,
                                   IPictureRepository pictureRepository,
                                   IRoasterTagRepository roasterTagRepository,
                                   ITagRepository tagRepository,
                                   IAddressRepository addressRepository,
                                   ILogger logger)
        {
            _roasterRepository = roasterRepository ?? throw new ArgumentNullException(nameof(roasterRepository));
            _roasterTagRepository = roasterTagRepository ?? throw new ArgumentNullException(nameof(roasterTagRepository));
            _pictureRepository = pictureRepository ?? throw new ArgumentNullException(nameof(pictureRepository));
            _tagRepository = tagRepository ?? throw new ArgumentNullException(nameof(tagRepository));
            _addressReposiotry = addressRepository ?? throw new ArgumentNullException(nameof(addressRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(addressRepository));
        }

        public async Task<int> AddRoasterAsync(Roaster roaster,
                                               string tags,
                                               Address address,
                                               string latitude,
                                               string longitude,
                                               IFormFile picture)
        {
            try
            {
                _logger.Information("Roaster admin service layer access in progress...");

                var roasterByName = await _roasterRepository.GetRoasterByNameNonTrackableAsync(roaster.Name);
                if (roasterByName != null)
                    return -1;

                //Get and process tags string into tag entities
                var _localTags = await RoasterAdminServiceBuilder.BuildTagsListAsync(tags,
                                                                                _tagRepository);

                //process address entity
                var _address = Address.New(address.AddressStr,
                                           address.OpeningHours,
                                           address.Latitude,
                                           address.Longitude);
                address = AddressCoordinatesTransformer.ConvertCoordinates(address, latitude, longitude);
                roaster.OfficeAddress = _address;
                _addressReposiotry.Add(_address);
                //add roasterTags  notes
                RoasterTagsPairsBuilder.BuildRoasterTags(_localTags,
                                                         roaster.Id,
                                                         _roasterTagRepository);

                var pictureBytes = BytePictureBuilder.GetBytePicture(picture);
                BytePictureBuilder.BindPicture(roaster.Id, pictureBytes, _pictureRepository);

                roaster = RoasterAdminServiceBuilder.AddRoasterNullPlugs(roaster);

                _roasterRepository.Add(roaster);
                await _roasterRepository.SaveChangesAsync();

                _logger.Information($"Roaster, Tags, RoasterTags, Addresses tables have been modified. Inserted roaster:\n Id: {roaster.Id}\n Roaster name: {roaster.Name}");
                return 0;
            }
            catch (Exception e)
            {
                _logger.Error($"Roaster admin service layer error occured! Error text message: {e.Message}");
                return -2;
            }
        }

        public async Task<int> DeleteRoasterByIdAsync(Guid id)
        {
            try
            {
                _logger.Information("Roaster admin service layer access in progress...");
                var roaster = await _roasterRepository.GetSingleAsync(id);
                _roasterRepository.Delete(roaster);
                var pairs = await _roasterTagRepository.GetPairsByRoasterIdAsNoTrackingAsync(id);
                _roasterTagRepository.DeleteRoasterTags(pairs);
                var picture = await _pictureRepository.GetPictureByRoasterIdAsyncAsNoTracking(id);
                if (picture != null)
                    _pictureRepository.Delete(picture);
                await _roasterRepository.SaveChangesAsync();
                _logger.Information($"Roaster, Tags, RoasterTags, Addresses tables have been modified. Deleted roaster:\n Id: {roaster.Id}\n Roaster name: {roaster.Name}");
                return 0;
            }
            catch (Exception e)
            {
                _logger.Error("Roaster admin service layer error occured! Error text message:" + e.Message);
                return -1;
            }
        }

        public async Task<IList<Roaster>> FetchRoastersAsync()
            {
            //Don't change ANYTHING! Multiple includes within repository leads to long-lasting load
            var roasters = await _roasterRepository.GetListAsync();
            var roastertags = await _roasterTagRepository.GetListAsync();
            foreach (var i in roasters)
                i.RoasterTags = roastertags.Where(rt => rt.RoasterId == i.Id).ToList();
            return roasters; 
    }

        public async Task<int> UpdateRoasterAsync(Roaster entity, string tags, string latitude, string longitude, IFormFile picture)
        { 
            _logger.Information("Roaster admin service layer access in progress...");
            try
            {
                var broast = await _roasterRepository.GetRoasterByNameNonTrackableAsync(entity.Name);
                if (broast != null && !broast.Id.Equals(entity.Id))
                    return -1;

                await RoasterAdminServiceBuilder.UpdateRoasterTagsAsync(tags,
                                        _tagRepository,
                                        _roasterTagRepository,
                                        entity.Id);

                    await BytePictureBuilder.ReplacePicture(entity.Id, picture, _pictureRepository);


                var roasterAddress = await _addressReposiotry.GetSingleAsync(entity.OfficeAddress.Id);
                entity.OfficeAddress = AddressCoordinatesTransformer.ConvertCoordinates(entity.OfficeAddress, latitude, longitude);
                entity.OfficeAddress = RoasterAdminServiceBuilder.UpdateRoasterAddress(roasterAddress, entity.OfficeAddress);
                _addressReposiotry.Update(entity.OfficeAddress);

                entity= RoasterAdminServiceBuilder.AddRoasterNullPlugs(entity);
                _roasterRepository.Update(entity);
                await _roasterTagRepository.SaveChangesAsync();
                _logger.Information($"Roaster, Tags, RoasterTags, Addresses tables have been modified. Updated roaster:\n Id: {entity.Id}\n Roaster name: {entity.Name}");
                return 0;
            }
            catch (Exception e)
            {
                _logger.Error("Roaster admin service layer error occured! Error text message:" + e.Message);
                return -2;
            }
        }

        public async Task<Roaster> FetchSingleRoasterAsync(Guid id)
            => await _roasterRepository.GetSingleAsync(id);
    }
}