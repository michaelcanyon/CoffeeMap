using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CoffeeMapServer.builders;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using CoffeeMapServer.Services.Interfaces.Admin;
using Microsoft.Extensions.Logging;

namespace CoffeeMapServer.Services
{
    public class RoasterRequestService : IRoasterRequestService
    {
        private readonly IRoasterRepository _roasterRepository;
        private readonly IRoasterRequestRepository _roasterRequestRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IRoasterTagRepository _roasterTagRepository;
        private readonly ILogger<RoasterRequestService> _logger;

        public RoasterRequestService(IRoasterRepository roasterRepository,
                                     IRoasterRequestRepository roasterRequestRepository,
                                     IAddressRepository addressRepository,
                                     ITagRepository tagRepository,
                                     IRoasterTagRepository roasterTagRepository,
                                     ILogger<RoasterRequestService> logger)
        {
            _roasterRepository = roasterRepository ?? throw new ArgumentNullException(nameof(roasterRepository));
            _roasterRequestRepository = roasterRequestRepository ?? throw new ArgumentNullException(nameof(roasterRequestRepository));
            _addressRepository = addressRepository ?? throw new ArgumentNullException(nameof(addressRepository));
            _tagRepository = tagRepository ?? throw new ArgumentNullException(nameof(tagRepository));
            _roasterTagRepository = roasterTagRepository ?? throw new ArgumentNullException(nameof(roasterTagRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task BindToRoasterNdAddressAsync(Guid id)
        {
            try
            {
                _logger.LogInformation("Roaster request service layer access in progress...");

                var request = await _roasterRequestRepository.GetSingleAsync(id);

                var bindTags = await RoasterRequestServiceBuilder.BuildAndBindTags(request.TagString, _tagRepository);

                var address = Address.New(request.Address.AddressStr, request.Address.OpeningHours);
                _addressRepository.Add(address);

                var roaster = RoasterRequestServiceBuilder.GenerateRoaster(request.Roaster, request.Address.Id);

                _roasterRepository.Add(roaster);

                RoasterTagsPairsBuilder.BuildRoasterTags(bindTags, roaster.Id, _roasterTagRepository);

                _roasterRequestRepository.Delete(request);

                await _roasterRequestRepository.SaveChangesAsync();

                _logger.LogInformation($"Roaster requests table has been modified. Roaster request:\n Id:{request.Id}");
            }
            catch (Exception e)
            {
                var em = new StringBuilder();
                em.AppendLine($"Roaster request service layer error occured! Error text message: {e.Message}");
                em.AppendLine($"Stack trace: {e.StackTrace}");
                _logger.LogError(em.ToString());
            }
        }

        public async Task DeleteAllRoasterRequestsAsync()
        {
            try
            {
                _logger.LogInformation("Roaster request service layer access in progress...");

                var range = await _roasterRequestRepository.GetListAsync();
                _roasterRequestRepository.DeleteRoasterRequest(range);
                await _roasterRequestRepository.SaveChangesAsync();

                _logger.LogInformation($"Roaster requests table has been cleared.");
            }
            catch (Exception e)
            {
                var em = new StringBuilder();
                em.AppendLine($"Roaster request service layer error occured! Error text message: {e.Message}");
                em.AppendLine($"Stack trace: {e.StackTrace}");
                _logger.LogError(em.ToString());
            }
        }

        public async Task DeleteRoasterRequestAsync(Guid id)
        {
            try
            {
                _logger.LogInformation("Roaster request service layer access in progress...");

                var roasterRequest = await _roasterRequestRepository.GetSingleAsync(id);
                _roasterRequestRepository.Delete(roasterRequest);
                await _roasterRequestRepository.SaveChangesAsync();

                _logger.LogInformation($"Roaster requests table has been modified. Deleted Roaster request:\n Id:{roasterRequest.Id}");
            }
            catch (Exception e)
            {
                var em = new StringBuilder();
                em.AppendLine($"Roaster request service layer error occured! Error text message: {e.Message}");
                em.AppendLine($"Stack trace: {e.StackTrace}");
                _logger.LogError(em.ToString());
            }
        }

        public async Task<IList<RoasterRequest>> FetchRoasterRequestsListAsync()
            => await _roasterRequestRepository.GetListAsync();

        public async Task<RoasterRequest> FetchSingleRoasterRequestByIdAsync(Guid id)
            => await _roasterRequestRepository.GetSingleAsync(id);

        public async Task UpdateRoasterRequestAsync(RoasterRequest entity)
        {
            try
            {
                _logger.LogInformation("Roaster request service layer access in progress...");

                _roasterRequestRepository.Update(entity);
                await _roasterRequestRepository.SaveChangesAsync();

                _logger.LogInformation($"Roaster requests table has been modified. Updated Roaster request:\n Id:{entity.Id}");
            }
            catch (Exception e)
            {
                var em = new StringBuilder();
                em.AppendLine($"Roaster request service layer error occured! Error text message: {e.Message}");
                em.AppendLine($"Stack trace: {e.StackTrace}");
                _logger.LogError(em.ToString());
            }
        }
    }
}