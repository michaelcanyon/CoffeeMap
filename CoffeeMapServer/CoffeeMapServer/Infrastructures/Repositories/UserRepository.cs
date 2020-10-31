using CoffeeMapServer.EF;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeMapServer.Infrastructures.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CoffeeDbContext Context;
        public UserRepository(CoffeeDbContext context)
        {
            Context = context;
        }
        public async Task Create(User entity)
        {
            entity.Password = CoffeeMapServer.Encryptions.Sha1Hash.GetHash(entity.Password);
            await Context.Users.AddAsync(entity);
            await Context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var user = (await Context.Users.Where(node => node.Id == id).ToListAsync()).First();
            Context.Remove(user);
            await Context.SaveChangesAsync();
        }

        public async Task<List<User>> GetList()
        {
            return await Context.Users.ToListAsync();
        }

        public async Task<User> GetSingle(Guid id)
        {
            var users = await Context.Users.Where(node => node.Id == id).ToListAsync();
            return users.Count() > 0 ? users.First() : null;
        }

        public async Task<User> GetSingle(string username, string password)
        {
            var users = await Context.Users.Where(node => (node.Login == username && node.Password == Encryptions.Sha1Hash.GetHash(password))).ToListAsync();
            return users.Count() > 0 ? users.First() : null;
        }

        public async Task<User> GetSingle(string username)
        {
            var users = await Context.Users.Where(node => node.Login == username).ToListAsync();
            return users.Count() > 0 ? users.First() : null;
        }

        public async Task<User> GetSingleByMail(string email)
        {
            var users = await Context.Users.Where(node => node.Email == email).ToListAsync();
            return users.Count() > 0 ? users.First() : null;
        }

        public async Task Update(User entity)
        {
            Context.Users.Update(entity);
            await Context.SaveChangesAsync();
        }
    }
}
