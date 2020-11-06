using CoffeeMapServer.Models;
using System.Threading.Tasks;

namespace CoffeeMapServer.Infrastructures.IRepositories
{
    public interface IAddressRepository : IBaseRepository<Address>
    {
        public Task<Address> GetSingleAsync(Address entity);
    }
}
