using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using CoffeeMapServer.Services.Interfaces;
using CoffeeMapServer.ViewModels;

namespace CoffeeMapServer.Services
{
    public class RoasterService : IRoasterService
    {
        private readonly IRoasterRepository _roasterRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IRoasterTagRepository _roasterTagRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IRoasterRequestRepository _roasterRequestRepository;

        public RoasterService(IRoasterRepository roasterRepository,
                              ITagRepository tagRepository,
                              IAddressRepository addressRepository,
                              IRoasterTagRepository roasterTagRepository,
                              IRoasterRequestRepository roasterRequestRepository)
        {
            _roasterRepository = roasterRepository ?? throw new ArgumentNullException(nameof(roasterRepository));
            _tagRepository = tagRepository ?? throw new ArgumentNullException(nameof(tagRepository));
            _roasterTagRepository = roasterTagRepository ?? throw new ArgumentNullException(nameof(roasterTagRepository));
            _addressRepository = addressRepository ?? throw new ArgumentNullException(nameof(addressRepository));
            _roasterRequestRepository = roasterRequestRepository ?? throw new ArgumentNullException(nameof(roasterRequestRepository));
        }

        public async Task<IList<Roaster>> GetRoastersAsync()
            => await _roasterRepository.GetListAsync();

        public async Task<RoasterInfoViewModel> GetRoasterViewModel(Guid id)
        {
            var roaster = await _roasterRepository.GetSingleAsync(id);
            var roasterAddress = await _addressRepository.GetSingleAsync(roaster.OfficeAddressId);
            var roasterTagsId = await _roasterTagRepository.GetPairsByRoasterIdAsync(roaster.Id);
            var tags = new List<Tag>();
            foreach (var item in roasterTagsId)
                tags.Add(await _tagRepository.GetSingleAsync(item.TagId));
            return new RoasterInfoViewModel(roaster, roasterAddress, tags);
        }

        public async Task SendRoasterRequest(RoasterRequest roasterRequest)
        {
            _roasterRequestRepository.Add(roasterRequest);
            await _roasterRequestRepository.SaveChangesAsync();
        }
    }
}