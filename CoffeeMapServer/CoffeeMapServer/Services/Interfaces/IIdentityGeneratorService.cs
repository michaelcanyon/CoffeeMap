using System.Security.Claims;
using System.Threading.Tasks;

namespace CoffeeMapServer.Services.Interfaces
{
    public interface IIdentityGeneratorService
    {
        public Task<ClaimsIdentity> GetIdentity(string username,
                                                string password);
    }
}