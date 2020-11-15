using System.Collections.Generic;
using CoffeeMapServer.Models;

namespace CoffeeMapServer.Infrastructures.IRepositories
{
    public interface IRoasterRequestRepository : IBaseRepository<RoasterRequest>
    {
        public void DeleteRoasterRequest(IList<RoasterRequest> range);
    }
}