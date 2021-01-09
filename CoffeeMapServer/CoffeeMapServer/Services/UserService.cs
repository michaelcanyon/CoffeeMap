using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using CoffeeMapServer.Services.Interfaces.Admin;
using Microsoft.Extensions.Logging;

namespace CoffeeMapServer.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository,
                           ILogger<UserService> logger)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<User> Login(string username, string password)
        {
            _logger.LogInformation("User service layer access in progress...");

            var hash = Encryptions.Sha1Hash.GetHash(password);
            return await _userRepository.GetSingleAsync(username, hash);
        }

        public async Task AddUserAsync(User entity)
        {
            try
            {
                _logger.LogInformation("User service layer access in progress...");

                User userV = await _userRepository.GetSingleAsync(entity.Login);
                if (userV != null)
                    return;

                userV = await _userRepository.GetSingleByMailAsync(entity.Email);
                if (userV != null)
                    return;

                _userRepository.Add(entity);
                await _userRepository.SaveChangesAsync();

                _logger.LogInformation($"Users table has been modified. Inserted user:\n Id:{entity.Id}\n Username: {entity.Login}");

            }
            catch (Exception e)
            {
                var em = new StringBuilder();
                em.AppendLine($"User service layer error occured! Error text message: {e.Message}");
                em.AppendLine($"Stack trace: {e.StackTrace}");
                _logger.LogError(em.ToString());
            }
        }

        public async Task DeleteUserAsync(Guid id)
        {
            try
            {
                _logger.LogInformation("User service layer access in progress...");

                var user = await _userRepository.GetSingleAsync(id);
                _userRepository.Delete(user);
                await _userRepository.SaveChangesAsync();

                _logger.LogInformation($"Users table has been modified. Deleted user:\n Id:{user.Id}\n Username: {user.Login}");
            }
            catch (Exception e)
            {
                var em = new StringBuilder();
                em.AppendLine($"User service layer error occured! Error text message: {e.Message}");
                em.AppendLine($"Stack trace: {e.StackTrace}");
                _logger.LogError(em.ToString());
            }
        }

        public async Task<IList<User>> FetchUsersAsync()
            => await _userRepository.GetListAsync();
    }
}