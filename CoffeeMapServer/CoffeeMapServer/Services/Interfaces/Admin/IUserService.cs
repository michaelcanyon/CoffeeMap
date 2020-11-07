using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeMapServer.Models;

namespace CoffeeMapServer.Services.Interfaces.Admin
{
   public interface IUserService
    {
        public Task<User> Login(string username, string hashedPassword);

        public Task AddUserAsync(User entity);

        public Task DeleteUserAsync(Guid id);

        public Task<IList<User>> FetchUsersAsync();


    }
}
