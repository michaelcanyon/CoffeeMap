using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using CoffeeMapServer.Services.Interfaces.Admin;

namespace CoffeeMapServer.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IRoasterRepository _roasterRepository;

        public AddressService(IAddressRepository addressRepository,
                              IRoasterRepository roasterRepository)
        {
            _addressRepository = addressRepository ?? throw new ArgumentNullException(nameof(addressRepository));
            _roasterRepository = roasterRepository ?? throw new ArgumentNullException(nameof(roasterRepository));
        }

        public async Task AddAddressAsync(Address entity)
        {
            //TODO: check if id generates automaticly
            _addressRepository.Add(entity);
            await _addressRepository.SaveChangesAsync();
        }

        public async Task DeleteAddressAsync(Guid id)
        {
            var address = await _addressRepository.GetSingleAsync(id);
            _addressRepository.Delete(address);

            var roasters = await _roasterRepository.FetchRoastersByAddressIdAsync(id);
            foreach (var item in roasters)
            {
                //TODO: 
                item.OfficeAddress = null;
                _roasterRepository.Update(item);
            }

            await _roasterRepository.SaveChangesAsync();
        }

        public async Task<IList<Address>> FetchAddressesAsync()
            => await _addressRepository.GetListAsync();

        public async Task<Address> GetSingleAddressByIdAsync(Guid id)
            => await _addressRepository.GetSingleAsync(id);

        public async Task UpdateAddressAsync(Address entity)
        {
            _addressRepository.Update(entity);
           await  _addressRepository.SaveChangesAsync();
        }
    }
}