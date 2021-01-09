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
                var em = new StringBuilder();
                em.AppendLine($"Account service layer error occured! Error text message: {e.Message}");
                em.AppendLine($"Stack trace: {e.StackTrace}");
                _logger.LogError(em.ToString());
                return null;
            }
        }

        public async Task UpdateAccountAsync(User entity,
                                             string newPasswordHash,
                                             string email)
        {
            try
            {
                entity.Password = newPasswordHash ?? entity.Password;
                entity.Email = email;
                _userRepository.Update(entity);
                await _userRepository.SaveChangesAsync();
                _logger.LogInformation("Account service Layer access in progress...");
                _logger.LogInformation("Account {0} has been modified", entity.Login);
            }
            catch (Exception e)
            {
                var em = new StringBuilder();
                em.AppendLine($"Account service layer error occured! Error text message: {e.Message}");
                em.AppendLine($"Stack trace: {e.StackTrace}");
                _logger.LogError(em.ToString());
            }
        }
    }
}