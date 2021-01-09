using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using CoffeeMapServer.Services.Interfaces.Admin;
using Microsoft.Extensions.Logging;

namespace CoffeeMapServer.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IRoasterRepository _roasterRepository;
        private readonly ILogger<AddressService> _logger;

        public AddressService(IAddressRepository addressRepository,
                              IRoasterRepository roasterRepository, ILogger<AddressService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _addressRepository = addressRepository ?? throw new ArgumentNullException(nameof(addressRepository));
            _roasterRepository = roasterRepository ?? throw new ArgumentNullException(nameof(roasterRepository));
        }

        public async Task AddAddressAsync(Address entity)
        {
            try
            {
                _logger.LogInformation("Address service Layer access in progress...");
                _addressRepository.Add(entity);
                await _addressRepository.SaveChangesAsync();
                _logger.LogInformation($"Address table has been modified! Address:\n Id: {entity.Id}\n Address string: {entity.AddressStr}\n has been inserted.");
            }
            catch (Exception e)
            {
                var em = new StringBuilder();
                em.AppendLine($"Address service layer error occured! Error message: {e.Message}");
                em.AppendLine($"Stack trace: {e.StackTrace}");
                _logger.LogError(em.ToString());

            }
        }

        public async Task DeleteAddressAsync(Guid id)
        {
            try
            {
                _logger.LogInformation("Address service Layer access in progress...");
                var address = await _addressRepository.GetSingleAsync(id);
                _addressRepository.Delete(address);

                var roasters = await _roasterRepository.FetchRoastersByAddressIdAsync(id);
                foreach (var item in roasters)
                {
                    item.OfficeAddress = null;
                    _roasterRepository.Update(item);
                }

                await _roasterRepository.SaveChangesAsync();
                _logger.LogInformation($"Address table has been modified. Address:\n Id: {address.Id}\n Address string: {address.AddressStr}\n has been deleted.");
            }
            catch (Exception e)
            {
                var em = new StringBuilder();
                em.AppendLine($"Address service layer error occured! Error message: {e.Message}");
                em.AppendLine($"Stack trace: {e.StackTrace}");
                _logger.LogError(em.ToString());
            }
        }

        public async Task<IList<Address>> FetchAddressesAsync()
            => await _addressRepository.GetListAsync();

        public async Task<Address> GetSingleAddressByIdAsync(Guid id)
            => await _addressRepository.GetSingleAsync(id);

        public async Task UpdateAddressAsync(Address entity)
        {
            try
            {
                _logger.LogInformation("Address service Layer access in progress...");
                _addressRepository.Update(entity);
                await _addressRepository.SaveChangesAsync();
                _logger.LogInformation($"Address Table has been modified. Address:\n Address Id: {entity.Id} \n Address string: {entity.AddressStr}\n has been added.");
            }
            catch(Exception e)
            {
                var em = new StringBuilder();
                em.AppendLine($"Address service layer error occured! Error message: {e.Message}");
                em.AppendLine($"Stack trace: {e.StackTrace}");
                _logger.LogError(em.ToString());
            }
        }
    }
}