using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CoffeeMapServer.builders;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using CoffeeMapServer.Services.Interfaces.Admin;
using Serilog;

namespace CoffeeMapServer.Services
{
    public class RoasterRequestService : IRoasterRequestService
    {
        private readonly IRoasterRepository _roasterRepository;
        private readonly IRoasterRequestRepository _roasterRequestRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IRoasterTagRepository _roasterTagRepository;
        private readonly ILogger _logger;

        public RoasterRequestService(IRoasterRepository roasterRepository,
                                     IRoasterRequestRepository roasterRequestRepository,
                                     IAddressRepository addressRepository,
                                     ITagRepository tagRepository,
                                     IRoasterTagRepository roasterTagRepository,
                                     ILogger logger)
        {
            _roasterRepository = roasterRepository ?? throw new ArgumentNullException(nameof(roasterRepository));
            _roasterRequestRepository = roasterRequestRepository ?? throw new ArgumentNullException(nameof(roasterRequestRepository));
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
                                          request.Address.OpeningHours);
                _addressRepository.Add(address);

                var roaster = RoasterRequestServiceBuilder.GenerateRoaster(request.Roaster,
                                                                           request.Address.Id);

                _roasterRepository.Add(roaster);

                RoasterTagsPairsBuilder.BuildRoasterTags(bindTags,
                                                         roaster.Id,
                                                         _roasterTagRepository);

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

        public async Task<int> UpdateRoasterRequestAsync(RoasterRequest entity)
        {
            try
            {
                _logger.Information("Roaster request service layer access in progress...");

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
    }
}