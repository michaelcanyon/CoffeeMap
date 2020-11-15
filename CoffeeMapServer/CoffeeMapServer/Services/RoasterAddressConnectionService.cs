using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using CoffeeMapServer.Services.Interfaces.Admin;

namespace CoffeeMapServer.Services
{
    public class RoasterAddressConnectionService : IRoasterAddressConnectionService
    {
        private readonly IRoasterRepository _roasterRepository;
        private readonly IAddressRepository _addressRepository;

        public RoasterAddressConnectionService(IRoasterRepository roasterRepository,
                                               IAddressRepository addressRepository)
        {
            _roasterRepository = roasterRepository ?? throw new ArgumentNullException(nameof(roasterRepository));
            _addressRepository = addressRepository ?? throw new ArgumentNullException(nameof(addressRepository));
        }

        public async Task<IList<Address>> FetchAddressesAsync()
            => await _addressRepository.GetListAsync();

        public async Task<IList<Roaster>> FetchRoastersAsync()
            => await _roasterRepository.GetListAsync();

        public async Task<Address> FetchSingleAddressByIdAsync(Guid id)
            => await _addressRepository.GetSingleAsync(id);

        public async Task<Roaster> FetchSingleRoasterByIdAsync(Guid id)
            => await _roasterRepository.GetSingleAsync(id);

        public async Task UpdateRoasterAsync(Roaster entity)
        {
            _roasterRepository.Update(entity);
            await _roasterRepository.SaveChangesAsync();
        }
    }
}