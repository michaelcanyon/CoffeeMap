using CoffeeMapServer.Models;
using System.Threading.Tasks;

namespace CoffeeMapServer.Infrastructures.IRepositories
{
    public interface IAddessRepository : IBaseRepository<Address>
    {
        public Task<Address> GetSingle(Address entity);
    }
}
