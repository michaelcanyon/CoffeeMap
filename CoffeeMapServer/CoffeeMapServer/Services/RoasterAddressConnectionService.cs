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
    public class RoasterAddressConnectionService : IRoasterAddressConnectionService
    {
        private readonly IRoasterRepository _roasterRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly ILogger<RoasterAddressConnectionService> _logger;

        public RoasterAddressConnectionService(IRoasterRepository roasterRepository,
                                               IAddressRepository addressRepository,
                                               ILogger<RoasterAddressConnectionService> logger)
        {
            _roasterRepository = roasterRepository ?? throw new ArgumentNullException(nameof(roasterRepository));
            _addressRepository = addressRepository ?? throw new ArgumentNullException(nameof(addressRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IList<Address>> FetchAddressesAsync()
            => await _addressRepository.GetListAsync();

        public async Task<IList<Roaster>> FetchRoastersAsync()
            => await _roasterRepository.GetListAsync();

        public async Task<Address> FetchSingleAddressByIdAsync(Guid id)
            => await _addressRepository.GetSingleAsync(id);

        public async Task<Roaster> FetchSingleRoasterByIdAsync(Guid id)
            => await _roasterRepository.GetSingleAsync(id);

        public async Task<int> UpdateRoasterAsync(Roaster entity)
        {
            try
            {
                _logger.LogInformation("RoasterAddressConnection service layer access in progress...");
                _roasterRepository.Update(entity);
                await _roasterRepository.SaveChangesAsync();
                _logger.LogInformation($"Roaster and Address tables have been modified. Modified roaster:\n {entity.Id}\n Name:{entity.Name}");
                return 0;
            }
            catch (Exception e)
            {
                _logger.LogError("Roaster address connection service layer error occured! Error text message:" + e.Message);
                return -1;
            }
        }
    }
}