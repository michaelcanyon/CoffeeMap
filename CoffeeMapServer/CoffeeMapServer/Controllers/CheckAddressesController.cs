using System.Threading.Tasks;
using CoffeeMapServer.Infrastructures.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeMapServer.Controllers
{
    [Route("/Admin/[controller]")]
    [ApiController]
    public class CheckAddressesController : Controller
    {
        private readonly IAddressRepository _addressRepository;

        public CheckAddressesController(IAddressRepository addressRepository)
            => _addressRepository = addressRepository;

        [HttpGet]
        [Route("Check")]
        public async Task<bool> CheckAddresses(string address)
        {
            var addressN = await _addressRepository.GetSingleAsNoTrackingAsync(address);
            return addressN == null ? false : true;
        }
    }
}
