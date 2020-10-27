using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using CoffeeMapServer.Services.Interfaces;
using CoffeeMapServer.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoffeeMapServer.Services
{
    public class RoasterService : IRoasterService
    {
        private readonly IRoasterRepository _roasterRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IRoasterTagRepository _roasterTagRepository;
        private readonly IAddessRepository _addressRepository;
        private readonly IRoasterRequestRepository _roasterRequestRepository;

        public RoasterService(IRoasterRepository roasterRepository,
                              ITagRepository tagRepository,
                              IAddessRepository addressRepository,
                              IRoasterTagRepository roasterTagRepository,
                              IRoasterRequestRepository roasterRequestRepository)
        {
            _roasterRepository = roasterRepository;
            _tagRepository = tagRepository;
            _roasterTagRepository = roasterTagRepository;
            _addressRepository = addressRepository;
            _roasterRequestRepository = roasterRequestRepository;
        }

        public async Task<List<Roaster>> GetRoasters()
            => await _roasterRepository.GetList();

        public async Task<RoasterInfoViewModel> GetSingleRoaster(Guid id)
        {
            var roaster = await _roasterRepository.GetSingle(id);
            var roasterAddress = await _addressRepository.GetSingle(roaster.OfficeAddressId);
            var roasterTagsId = await _roasterTagRepository.GetPairsByRoasterId(roaster.Id);
            List<Tag> tags = new List<Tag>();
            foreach (var item in roasterTagsId)
                tags.Add(await _tagRepository.GetSingle(item.TagId));
            return new RoasterInfoViewModel(roaster, roasterAddress, tags);
        }

        public async Task SendRoasterRequest(RoasterRequest roasterRequest)
            => await _roasterRequestRepository.Create(roasterRequest);
    }
}
