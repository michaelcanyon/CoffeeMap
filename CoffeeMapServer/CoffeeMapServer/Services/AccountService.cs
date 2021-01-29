using System;
using System.Threading.Tasks;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using CoffeeMapServer.Services.Interfaces.Admin;
using Serilog;

namespace CoffeeMapServer.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger _logger;

        public AccountService(IUserRepository userRepository, ILogger logger)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _logger = logger;
        }

        public async Task<User> GetAccountByIdAsync(Guid id)
        {
            try
            {
                var user = await _userRepository.GetSingleAsync(id);
                _logger.Information("Account service Layer access in progress...");
                _logger.Information("User {0}, {1}, Role: {2} access procedure implemented", user.Id, user.Login, user.Role);
                return user;
            }
            catch(Exception e)
            {
                _logger.Error($"Account service layer error occured! Error text message: {e.Message}");
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
                var user =await _userRepository.GetSingleByMailAsync(email);
                if (user != null && !user.Id.Equals(entity.Id))
                    return -1;
                _userRepository.Update(entity);
                await _userRepository.SaveChangesAsync();
                _logger.Information("Account service Layer access in progress...");
                _logger.Information("Account {0} has been modified", entity.Login);
                return 0;
            }
            catch (Exception e)
            {
                _logger.Error($"Account service layer error occured! Error text message: {e.Message}");
                return -2;
            }
        }
    }
}