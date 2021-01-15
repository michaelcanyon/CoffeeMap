﻿using System.Threading.Tasks;
using CoffeeMapServer.Models;

namespace CoffeeMapServer.Infrastructures.IRepositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        public Task<User> GetSingleAsync(string username, string password);

        public Task<User> GetSingleAsync(string username);

        public Task<User> GetSingleByMailAsync(string email);
    }
}