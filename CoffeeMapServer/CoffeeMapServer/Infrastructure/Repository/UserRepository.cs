using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CoffeeMapServer.EF;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeMapServer.Infrastructures.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CoffeeDbContext Context;

        public UserRepository(CoffeeDbContext context)
            => Context = context ?? throw new ArgumentNullException(nameof(CoffeeDbContext));

        public void Add(User entity)
            => Context.Users.Add(entity);

        public void Delete(User entity)
            => Context.Remove(entity);

        public async Task<IList<User>> GetListAsync([CallerMemberName] string methodName = "")
            => await Context.Users
               .TagWith($"{nameof(UserRepository)}.{methodName}")
               .ToListAsync();

        public async Task<User> GetSingleAsync(Guid id,
                                               [CallerMemberName] string methodName = "")
            => await Context.Users
               .TagWith($"{nameof(UserRepository)}.{methodName} ({id})")
               .FirstOrDefaultAsync(node => node.Id == id);

        public async Task<User> GetSingleAsync(string username,
                                               string hashedPassword,
                                               [CallerMemberName] string methodName = "")
            => await Context.Users
               .TagWith($"{nameof(UserRepository)}.{methodName} ({username})")
               .FirstOrDefaultAsync(node => (node.Login == username && node.Password == hashedPassword));

        public async Task<User> GetSingleAsync(string username,
                                               [CallerMemberName] string methodName = "")
            => await Context.Users
               .TagWith($"{nameof(UserRepository)}.{methodName} ({username})")
               .FirstOrDefaultAsync(node => (node.Login == username));

        public async Task<User> GetSingleByMailAsync(string email,
                                                     [CallerMemberName] string methodName = "")
            => await Context.Users
               .TagWith($"{nameof(UserRepository)}.{methodName} ({email})")
               .FirstOrDefaultAsync(node => node.Email == email);

        public void Update(User entity)
            => Context.Users.Update(entity);

        public async Task SaveChangesAsync()
            => await Context.SaveChangesAsync();
    }
}