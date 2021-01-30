using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CoffeeMapServer.Models;

namespace CoffeeMapServer.Infrastructures.IRepositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        public Task<User> GetSingleAsync(string username,
                                         string password,
                                         [CallerMemberName] string methodName = "");

        public Task<User> GetSingleAsync(string username,
                                         [CallerMemberName] string methodName = "");

        public Task<User> GetSingleByMailAsync(string email,
                                               [CallerMemberName] string methodName = "");
    }
}