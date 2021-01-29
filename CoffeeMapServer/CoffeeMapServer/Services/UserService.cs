using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using CoffeeMapServer.Services.Interfaces.Admin;
using Serilog;

namespace CoffeeMapServer.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;
        private readonly ILogger _logger;

        public UserService(IUserRepository userRepository,
                           ILogger logger)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<User> Login(string username, string password)
        {
            _logger.Information("User service layer access in progress...");

            var hash = Encryptions.Sha1Hash.GetHash(password);
            return await _userRepository.GetSingleAsync(username, hash);
        }

        public async Task<int> AddUserAsync(User entity)
        {
            try
            {
                _logger.Information("User service layer access in progress...");

                User userV = await _userRepository.GetSingleAsync(entity.Login);
                if (userV != null)
                    return 601;

                userV = await _userRepository.GetSingleByMailAsync(entity.Email);
                if (userV != null)
                    return 602;

                _userRepository.Add(entity);
                await _userRepository.SaveChangesAsync();

                _logger.Information($"Users table has been modified. Inserted user:\n Id:{entity.Id}\n Username: {entity.Login}");
                return 0;

            }
            catch (Exception e)
            {
                _logger.Error($"User service layer error occured! Error text message: {e.Message}");
                return -2;
            }
        }

        public async Task<int> DeleteUserAsync(Guid id)
        {
            try
            {
                _logger.Information("User service layer access in progress...");

                var user = await _userRepository.GetSingleAsync(id);
                _userRepository.Delete(user);
                await _userRepository.SaveChangesAsync();

                _logger.Information($"Users table has been modified. Deleted user:\n Id:{user.Id}\n Username: {user.Login}");
                return 0;
            }
            catch (Exception e)
            {
                _logger.Error($"User service layer error occured! Error text message: {e.Message}");
                return -1;
            }
        }

        public async Task<IList<User>> FetchUsersAsync()
            => await _userRepository.GetListAsync();
    }
}