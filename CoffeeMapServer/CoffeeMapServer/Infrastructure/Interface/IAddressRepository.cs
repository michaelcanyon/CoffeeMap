using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CoffeeMapServer.Models;

namespace CoffeeMapServer.Infrastructures.IRepositories
{
    public interface IAddressRepository : IBaseRepository<Address>
    {
        public Task<Address> GetSingleAsync(string addressStr,
                                            [CallerMemberName] string methodName = "");

        public Task<Address> GetSingleAsNoTrackingAsync(string addressStr,
                                                        [CallerMemberName] string methodName = "");
    }
}