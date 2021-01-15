using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeMapServer.Models;

namespace CoffeeMapServer.Services.Interfaces.Admin
{
    public interface IUserService
    {
        public Task<User> Login(string username, string hashedPassword);

        public Task<int> AddUserAsync(User entity);

        public Task<int> DeleteUserAsync(Guid id);

        public Task<IList<User>> FetchUsersAsync();
    }
}