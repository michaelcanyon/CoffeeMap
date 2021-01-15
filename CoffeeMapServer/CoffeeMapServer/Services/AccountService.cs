using System;
using System.Text;
using System.Threading.Tasks;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using CoffeeMapServer.Services.Interfaces.Admin;
using Microsoft.Extensions.Logging;

namespace CoffeeMapServer.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<AccountService> _logger;

        public AccountService(IUserRepository userRepository, ILogger<AccountService> logger)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _logger = logger;
        }

        public async Task<User> GetAccountByIdAsync(Guid id)
        {
            try
            {
                var user = await _userRepository.GetSingleAsync(id);
                _logger.LogInformation("Account service Layer access in progress...");
                _logger.LogInformation("User {0}, {1}, Role: {2} access procedure implemented", user.Id, user.Login, user.Role);
                return user;
            }
            catch(Exception e)
            {
                _logger.LogError($"Account service layer error occured! Error text message: {e.Message}");
                return null;
            }
        }

        public async Task<int> UpdateAccountAsync(User entity,
                                             string newPasswordHash,
                                             string email)
        {
            try
            {
                entity.Password = newPasswordHash ?? entity.Password;
                entity.Email = email;
                var user = _userRepository.GetSingleByMailAsync(email);
                if (user != null)
                    return -1;
                _userRepository.Update(entity);
                await _userRepository.SaveChangesAsync();
                _logger.LogInformation("Account service Layer access in progress...");
                _logger.LogInformation("Account {0} has been modified", entity.Login);
                return 0;
            }
            catch (Exception e)
            {
                _logger.LogError($"Account service layer error occured! Error text message: {e.Message}");
                return -2;
            }
        }
    }
}