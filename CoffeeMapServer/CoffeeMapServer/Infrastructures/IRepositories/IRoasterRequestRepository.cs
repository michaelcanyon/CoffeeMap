using CoffeeMapServer.Models;
using System.Threading.Tasks;

namespace CoffeeMapServer.Infrastructures.IRepositories
{
    public interface IRoasterRequestRepository : IBaseRepository<RoasterRequest>
    {
        public Task DeleteAll();
    }
}
