using System;
using System.Threading.Tasks;
using CoffeeMapServer.Models;

namespace CoffeeMapServer.Services.Interfaces.Admin
{
    public interface IAccountService
    {
        public Task<User> GetAccountByIdAsync(Guid id);

        public Task UpdateAccountAsync(
            User entity,
            string newPasswordHash,
            string email);
    }
}
