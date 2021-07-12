using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeMapServer.builders;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using CoffeeMapServer.Services.Interfaces.Admin;
using Serilog;

namespace CoffeeMapServer.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IRoasterRepository _roasterRepository;
        private readonly ILogger _logger;

        public AddressService(IAddressRepository addressRepository,
                              IRoasterRepository roasterRepository,
                              ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _addressRepository = addressRepository ?? throw new ArgumentNullException(nameof(addressRepository));
            _roasterRepository = roasterRepository ?? throw new ArgumentNullException(nameof(roasterRepository));
        }

        public async Task<int> AddAddressAsync(Address entity, string latitude, string longitude)
        {
            try
            {

                entity = AddressCoordinatesTransformer.ConvertCoordinates(entity, latitude, longitude);
                _logger.Information("Address service Layer access in progress...");
                var dbAddress = await _addressRepository.GetSingleAsNoTrackingAsync(entity.AddressStr);
                if (dbAddress != null)
                    return -1;
                if (entity.OpeningHours == null)
                    entity.OpeningHours = "none";
                _addressRepository.Add(entity);
                await _addressRepository.SaveChangesAsync();
                _logger.Information($"Address table has been modified! Address:\n Id: {entity.Id}\n Address string: {entity.AddressStr}\n has been inserted.");
                return 0;
            }
            catch (Exception e)
            {
                _logger.Error($"Address service layer error occured! Error message: {e.Message}");
                return -2;
            }
        }

        public async Task<int> DeleteAddressAsync(Guid id)
        {
            try
            {
                _logger.Information("Address service Layer access in progress...");
                var address = await _addressRepository.GetSingleAsync(id);
                _addressRepository.Delete(address);

                var roasters = await _roasterRepository.FetchRoastersByAddressIdAsync(id);
                foreach (var item in roasters)
                {
                    item.OfficeAddress = Address.New(Guid.NewGuid(), "none", null, 0, 0);
                    _addressRepository.Add(item.OfficeAddress);
                    _roasterRepository.Update(item);
                }

                await _roasterRepository.SaveChangesAsync();
                _logger.Information($"Address table has been modified. Address:\n Id: {address.Id}\n Address string: {address.AddressStr}\n has been deleted.");
                return 0;
            }
            catch (Exception e)
            {
                _logger.Error("Address service layer error occured! Error message:" + e.Message);
                return -1;
            }
        }

        public async Task<IList<Address>> FetchAddressesAsync()
            => await _addressRepository.GetListAsync();

        public async Task<Address> GetSingleAddressByIdAsync(Guid id)
            => await _addressRepository.GetSingleAsync(id);

        public async Task<int> UpdateAddressAsync(Address entity, string latitude, string longitude)
        {
            try
            {
                entity = AddressCoordinatesTransformer.ConvertCoordinates(entity, latitude, longitude);
                _logger.Information("Address service Layer access in progress...");
                var address = await _addressRepository.GetSingleAsNoTrackingAsync(entity.AddressStr);
                if (address != null && !address.Id.Equals(entity.Id))
                    return -1;
                if (entity.OpeningHours == null)
                    entity.OpeningHours = "none";
                _addressRepository.Update(entity);
                await _addressRepository.SaveChangesAsync();
                _logger.Information($"Address Table has been modified. Address:\n Address Id: {entity.Id} \n Address string: {entity.AddressStr}\n has been added.");
                return 0;
            }
            catch (Exception e)
            {
                _logger.Error("Address service layer error occured! Error message:" + e.Message);
                return -2;
            }
        }
    }
}