using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;

namespace CoffeeMapServer.Infrastructure.Interface
{
    public interface IPictureRequestRepository : IBaseRepository<PictureRequest>
    {
        public Task<PictureRequest> GetPictureReqByRoasterReqIdAsyncAsNoTracking(Guid roasterId);
    }
}
