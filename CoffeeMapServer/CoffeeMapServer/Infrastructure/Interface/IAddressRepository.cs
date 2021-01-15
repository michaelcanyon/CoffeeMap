using System.Threading.Tasks;
using CoffeeMapServer.Models;

namespace CoffeeMapServer.Infrastructures.IRepositories
{
    public interface IAddressRepository : IBaseRepository<Address>
    {
        public Task<Address> GetSingleAsync(string addressStr);
    }
}