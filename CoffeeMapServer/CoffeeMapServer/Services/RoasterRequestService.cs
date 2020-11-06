using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using CoffeeMapServer.Models.Intermediary_models;
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
            _roasterRepository = roasterRepository;
            _roasterRequestRepository = roasterRequestRepository;
            _addressRepository = addressRepository;
            _tagRepository = tagRepository;
            _roasterTagRepository = roasterTagRepository;

        }

        public async Task BindToRoasterNdAddressAsync(Guid id)
        {
            var request = await _roasterRequestRepository.GetSingleAsync(id);
            var tags = request.TagString.Split(";");
            var bindTags = new List<Tag>();
            foreach (var item in tags)
            {//TODO: condition made to avoid excessive tag getting function call
                if (await _tagRepository.GetSingleAsync(item) == null)
                {
                    var tag = Tag.New(item);
                    _tagRepository.Add(tag);
                    bindTags.Add(tag);
                    continue;
                }
                bindTags.Add(await _tagRepository.GetSingleAsync(item));
            }
            var address = Address.New(request.AddressStr, request.OpeningHours);
            _addressRepository.Add(address);

            var roaster = Roaster.New(
                request.Name,
                request.ContactNumber,
                request.ContactEmail,
                address.Id,
                request.WebSiteLink,
                request.VkProfileLink,
                request.InstagramProfileLink,
                request.TelegramProfileLink,
                request.Picture,
                request.Description);

            _roasterRepository.Add(roaster);

            foreach (var item in bindTags)
                _roasterTagRepository.Add(new RoasterTag(roaster.Id, item.Id));

            _roasterRequestRepository.Delete(request);


            //TODO: learn how to implement folowing statements as one transaction
            await _roasterRequestRepository.SaveChangesAsync();
            await _roasterRepository.SaveChangesAsync();
            await _addressRepository.SaveChangesAsync();
            await _roasterTagRepository.SaveChangesAsync();
            await _tagRepository.SaveChangesAsync();
        }

        public async Task DeleteAllRoasterRequestsAsync()
        {
            var range = await _roasterRequestRepository.GetListAsync();
            _roasterRequestRepository.DeleteRange(range);
            await _roasterRequestRepository.SaveChangesAsync();
        }

        public async Task DeleteRoasterRequestAsync(Guid id)
        {
            var roasterRequest = await _roasterRequestRepository.GetSingleAsync(id);
            _roasterRequestRepository.Delete(roasterRequest);
            await _roasterRequestRepository.SaveChangesAsync();
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
