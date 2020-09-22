using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Infrastructures.Repositories;
using CoffeeMapServer.Models;
using CoffeeMapServer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public RoasterService(IRoasterRepository roasterRepository, ITagRepository tagRepository,
            IAddessRepository addressRepository, IRoasterTagRepository roasterTagRepository, IRoasterRequestRepository roasterRequestRepository)
        {
            _roasterRepository = roasterRepository;
            _tagRepository = tagRepository;
            _roasterTagRepository = roasterTagRepository;
            _addressRepository = addressRepository;
            _roasterRequestRepository = roasterRequestRepository;
        }
        public async Task<List<Roaster>> GetRoasters()
        {
            var roasters = await _roasterRepository.GetList();
            return roasters;
        }

        public async Task<RoasterInfoModel> GetSingleRoaster(int id)
        {
            RoasterInfoModel roasterInfo = new RoasterInfoModel();
            roasterInfo.roaster = await _roasterRepository.GetSingle(id);
            roasterInfo.address = await _addressRepository.GetSingle(roasterInfo.roaster.OfficeAddressId);
            var roasterTagsId = await _roasterTagRepository.GetPairsByRoasterId(roasterInfo.roaster.Id);
            foreach (var item in roasterTagsId)
                roasterInfo.tags.Add(await _tagRepository.GetSingle(item.TagId));
            return roasterInfo;
        }

        public async Task PostRoasterRequest(RoasterRequest roasterRequest)
        {
            await _roasterRequestRepository.Create(roasterRequest);
        }
    }
}
