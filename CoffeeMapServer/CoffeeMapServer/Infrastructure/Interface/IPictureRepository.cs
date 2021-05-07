using System;
using System.Threading.Tasks;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;

namespace CoffeeMapServer.Infrastructure.Interface
{
    public interface IPictureRepository : IBaseRepository<Picture>
    {
        public Task<Picture> GetPictureByRoasterIdAsyncAsNoTracking(Guid roasterId);
    }
}
