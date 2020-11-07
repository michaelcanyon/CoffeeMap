using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using CoffeeMapServer.Services.Interfaces.Admin;

namespace CoffeeMapServer.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
            => _userRepository = userRepository;

        public async Task<User> Login(string username, string password)
        { 
            var hash = Encryptions.Sha1Hash.GetHash(password);
            return await _userRepository.GetSingleAsync(username, hash); 
        }

        public async Task AddUserAsync(User entity)
        {
            User userV = await _userRepository.GetSingleAsync(entity.Login);
            if (userV != null)
                return;
            userV = await _userRepository.GetSingleByMailAsync(entity.Email);
            if (userV != null)
                return;
            _userRepository.Add(entity);
            await _userRepository.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var user = await _userRepository.GetSingleAsync(id);
            _userRepository.Delete(user);
            await _userRepository.SaveChangesAsync();
        }

        public async Task<IList<User>> FetchUsersAsync()
            => await _userRepository.GetListAsync();
    }
}
