using CoffeeMapServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeMapServer.Infrastructures.IRepositories
{
  public interface IUserRepository: IBaseRepository<User>
    {
        public Task<User> GetSingle(string username, string password);
    }
}
