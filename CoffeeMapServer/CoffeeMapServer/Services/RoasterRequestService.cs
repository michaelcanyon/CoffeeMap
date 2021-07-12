using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeMapServer.builders;
using CoffeeMapServer.Infrastructure.Interface;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using CoffeeMapServer.Services.Interfaces.Admin;
using CoffeeMapServer.ViewModels.DTO;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace CoffeeMapServer.Services
{
    public class RoasterRequestService : IRoasterRequestService
    {
        private readonly IRoasterRepository _roasterRepository;
        private readonly IRoasterRequestRepository _roasterRequestRepository;
        private readonly IPictureRepository _pictureRepository;
        private readonly IPictureRequestRepository _pictureRequestRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IRoasterTagRepository _roasterTagRepository;
        private readonly ILogger _logger;

        public RoasterRequestService(IRoasterRepository roasterRepository,
                                     IRoasterRequestRepository roasterRequestRepository,
                                     IPictureRequestRepository pictureRequestRepository,
                                     IPictureRepository pictureRepository,
                                     IAddressRepository addressRepository,
                                     ITagRepository tagRepository,
                                     IRoasterTagRepository roasterTagRepository,
                                     ILogger logger)
        {
            _roasterRepository = roasterRepository ?? throw new ArgumentNullException(nameof(roasterRepository));
            _roasterRequestRepository = roasterRequestRepository ?? throw new ArgumentNullException(nameof(roasterRequestRepository));
            _pictureRepository = pictureRepository;
            _pictureRequestRepository = pictureRequestRepository;
            _addressRepository = addressRepository ?? throw new ArgumentNullException(nameof(addressRepository));
            _tagRepository = tagRepository ?? throw new ArgumentNullException(nameof(tagRepository));
            _roasterTagRepository = roasterTagRepository ?? throw new ArgumentNullException(nameof(roasterTagRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<int> BindToRoasterNdAddressAsync(Guid id)
        {
            try
            {
                _logger.Information("Roaster request service layer access in progress...");

                var request = await _roasterRequestRepository.GetSingleAsync(id);

                var bindTags = await RoasterRequestServiceBuilder.BuildAndBindTags(request.TagString,
                                                                                   _tagRepository);

                var address = Address.New(request.Address.AddressStr,
                                          request.Address.OpeningHours,
                                          request.Address.Latitude,
                                          request.Address.Longitude);

                var roaster = RoasterRequestServiceBuilder.GenerateRoaster(request.Roaster);

                roaster.OfficeAddress = address;

                //Раскоменти, если не заработает
                //_addressRepository.Add(address);

                BytePictureBuilder.BindPicture(roaster.Id, request.Picture.Bytes, _pictureRepository);

                _roasterRepository.Add(roaster);

                RoasterTagsPairsBuilder.BuildRoasterTags(bindTags,
                                                         roaster.Id,
                                                         _roasterTagRepository);

                _pictureRequestRepository.Delete(request.Picture);
                _roasterRequestRepository.Delete(request);

                await _roasterRequestRepository.SaveChangesAsync();

                _logger.Information($"Roaster requests table has been modified. Roaster request:\n Id:{request.Id}");
                return 0;
            }
            catch (Exception e)
            {
                _logger.Error($"Roaster request service layer error occured! Error text message: {e.Message}");
                return -1;
            }
        }

        public async Task<int> DeleteAllRoasterRequestsAsync()
        {
            try
            {
                _logger.Information("Roaster request service layer access in progress...");

                var range = await _roasterRequestRepository.GetListAsync();
                _roasterRequestRepository.DeleteRoasterRequest(range);
                await _roasterRequestRepository.SaveChangesAsync();

                _logger.Information($"Roaster requests table has been cleared.");
                return 0;
            }
            catch (Exception e)
            {
                _logger.Error($"Roaster request service layer error occured! Error text message: {e.Message}");
                return -1;
            }
        }

        public async Task<int> DeleteRoasterRequestAsync(Guid id)
        {
            try
            {
                _logger.Information("Roaster request service layer access in progress...");

                var roasterRequest = await _roasterRequestRepository.GetSingleAsync(id);
                _roasterRequestRepository.Delete(roasterRequest);
                await _roasterRequestRepository.SaveChangesAsync();

                _logger.Information($"Roaster requests table has been modified. Deleted Roaster request:\n Id:{roasterRequest.Id}");
                return 0;
            }
            catch (Exception e)
            {
                _logger.Error($"Roaster request service layer error occured! Error text message: {e.Message}");
                return -1;
            }
        }

        public async Task<IList<RoasterRequest>> FetchRoasterRequestsListAsync()
            => await _roasterRequestRepository.GetListAsync();

        public async Task<RoasterRequest> FetchSingleRoasterRequestByIdAsync(Guid id)
            => await _roasterRequestRepository.GetSingleAsync(id);

        public async Task<int> UpdateRoasterRequestAsync(RoasterRequest entity,
                                                         IFormFile picture)
        {
            try
            {
                _logger.Information("Roaster request service layer access in progress...");

                await BytePictureBuilder.ReplacePictureRequest(entity.Id,
                                                               picture,
                                                               _pictureRequestRepository);

                _roasterRequestRepository.Update(entity);
                await _roasterRequestRepository.SaveChangesAsync();

                _logger.Information($"Roaster requests table has been modified. Updated Roaster request:\n Id:{entity.Id}");
                return 0;
            }
            catch (Exception e)
            {
                _logger.Error($"Roaster request service layer error occured! Error text message: {e.Message}");
                return -1;
            }
        }

        public async Task SendRoasterRequest(RoasterRequestDT roasterRequestDT)
        {
            try
            {
                _logger.Information("Roaster service layer access in progress...");

                var roasterRequest = RoasterRequestServiceBuilder.GenerateRoasterRequest(roasterRequestDT,
                                                                                         _pictureRequestRepository);
                _roasterRequestRepository.Add(roasterRequest);
                await _roasterRequestRepository.SaveChangesAsync();

                _logger.Information($"Roaster request table has been modified. Inserted request:\n Id:{roasterRequest.Id}");
            }
            catch (Exception e)
            {
                _logger.Error(e, "Roaster service layer error occured!");
            }
        }
    }
}