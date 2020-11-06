using System;
using System.Collections.Generic;
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
            => Context = context;
        /// <summary>
        /// Don't forget to add HASHING
        /// </summary>
        /// <param name="entity"></param>
        public void Add(User entity)
            => Context.Users.Add(entity);

        public void Delete(User entity)
            => Context.Remove(entity);

        public async Task<IList<User>> GetListAsync()
            => await Context.Users.ToListAsync();

        public async Task<User> GetSingleAsync(Guid id)
            => await Context.Users.FirstOrDefaultAsync(node => node.Id == id);

        /// <summary>
        /// hash password before check
        /// </summary>
        /// <param name="username"></param>
        /// <param name="hashedpassword"></param>
        /// <returns></returns>
        public async Task<User> GetSingleAsync(string username, string hashedPassword)
            => await Context.Users.FirstOrDefaultAsync(node => (node.Login == username && node.Password == hashedPassword));

        public async Task<User> GetSingleAsync(string username)
            => await Context.Users.FirstOrDefaultAsync(node => node.Login == username);

        public async Task<User> GetSingleByMailAsync(string email)
            => await Context.Users.FirstOrDefaultAsync(node => node.Email == email);

        public void Update(User entity)
            => Context.Users.Update(entity);

        public async Task SaveChangesAsync()
            => await Context.SaveChangesAsync();
    }
}
