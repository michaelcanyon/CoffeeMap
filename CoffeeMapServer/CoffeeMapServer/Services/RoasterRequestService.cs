using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeMapServer.builders;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using CoffeeMapServer.Services.Interfaces.Admin;

namespace CoffeeMapServer.Services
{
    public class RoasterRequestService : IRoasterRequestService
    {
        private readonly IRoasterRepository _roasterRepository;
        private readonly IRoasterRequestRepository _roasterRequestRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IRoasterTagRepository _roasterTagRepository;

        public RoasterRequestService(
            IRoasterRepository roasterRepository,
            IRoasterRequestRepository roasterRequestRepository,
            IAddressRepository addressRepository,
            ITagRepository tagRepository, IRoasterTagRepository roasterTagRepository)
        {
            _roasterRepository = roasterRepository ?? throw new ArgumentNullException(nameof(roasterRepository));
            _roasterRequestRepository = roasterRequestRepository ?? throw new ArgumentNullException(nameof(roasterRequestRepository));
            _addressRepository = addressRepository ?? throw new ArgumentNullException(nameof(addressRepository));
            _tagRepository = tagRepository ?? throw new ArgumentNullException(nameof(tagRepository));
            _roasterTagRepository = roasterTagRepository ?? throw new ArgumentNullException(nameof(roasterTagRepository));

        }

        public async Task BindToRoasterNdAddressAsync(Guid id)
        {
            var request = await _roasterRequestRepository.GetSingleAsync(id);

            var bindTags = await RoasterRequestServiceBuilder.BuildAndBindTags(request.TagString, _tagRepository);

            var address = Address.New(request.Address.AddressStr, request.Address.OpeningHours);
            _addressRepository.Add(address);

            var roaster = RoasterRequestServiceBuilder.GenerateRoaster(request.Roaster, request.Address.Id);

            _roasterRepository.Add(roaster);

            foreach (var item in bindTags)
                _roasterTagRepository.Add(new RoasterTag(roaster.Id, item.Id));

            _roasterRequestRepository.Delete(request);

            await _roasterRequestRepository.SaveChangesAsync();
        }

        public async Task DeleteAllRoasterRequestsAsync()
        {
            var range = await _roasterRequestRepository.GetListAsync();
            _roasterRequestRepository.DeleteRoasterRequest(range);
            await _roasterRequestRepository.SaveChangesAsync();
        }

        public async Task DeleteRoasterRequestAsync(Guid id)
        {
            var roasterRequest = await _roasterRequestRepository.GetSingleAsync(id);
            _roasterRequestRepository.Delete(roasterRequest);
            await _roasterRequestRepository.SaveChangesAsync();
        }

        public async Task<IList<RoasterRequest>> FetchRoasterRequestsListAsync()
            => await _roasterRequestRepository.GetListAsync();

        public async Task<RoasterRequest> FetchSingleRoasterRequestByIdAsync(Guid id)
            => await _roasterRequestRepository.GetSingleAsync(id);

        public async Task UpdateRoasterRequestAsync(RoasterRequest entity)
        {
            _roasterRequestRepository.Update(entity);
            await _roasterRequestRepository.SaveChangesAsync();
        }
    }
}