using System;
using System.Threading.Tasks;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using CoffeeMapServer.Services.Interfaces.Admin;

namespace CoffeeMapServer.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;

        public AccountService(IUserRepository userRepository)
            => _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));

        public async Task<User> GetAccountByIdAsync(Guid id)
            => await _userRepository.GetSingleAsync(id);

        public async Task UpdateAccountAsync(User entity,
                                             string newPasswordHash,
                                             string email)
        {
            entity.Password = newPasswordHash ?? entity.Password;
            entity.Email = email;
            _userRepository.Update(entity);
            await _userRepository.SaveChangesAsync();
        }
    }
}