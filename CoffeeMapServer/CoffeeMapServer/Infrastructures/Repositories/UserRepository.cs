using CoffeeMapServer.EF;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeMapServer.Infrastructures.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CoffeeDbContext dbContext;
        public UserRepository(CoffeeDbContext context)
        {
            dbContext = context;
        }
        public async Task Create(User entity)
        {
            entity.Password = CoffeeMapServer.Encryptions.Sha1Hash.GetHash(entity.Password);
            await dbContext.Users.AddAsync(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            SqlParameter _idParam = new SqlParameter("@id", id);
            await dbContext.Database.ExecuteSqlRawAsync("DELETE FROM Users WHERE Id=@id", _idParam);
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<User>> GetList()
        {
           return await dbContext.Users.ToListAsync();
        }

        public async Task<User> GetSingle(int id)
        {
            SqlParameter _idParam = new SqlParameter("@id", id);
           var users= await dbContext.Users.FromSqlRaw("SELECT * FROM Users WHERE Id=@id", _idParam).ToListAsync();
            return users.Count() > 0 ? users.First() : null; 
        }

        public async Task<User> GetSingle(string username, string password)
        {
            SqlParameter _username = new SqlParameter("@username", username);
            var passwordHash = CoffeeMapServer.Encryptions.Sha1Hash.GetHash(password);
            SqlParameter _password = new SqlParameter("@password", passwordHash);
            var user =await dbContext.Users.FromSqlRaw("SELECT * FROM Users WHERE Login=@username AND Password=@password", _username, _password).ToListAsync();
            return user.Count() > 0 ? user.First() : null;
        }
        public async Task<User> GetSingle(string username)
        {
            SqlParameter _username = new SqlParameter("@username", username);
            var user = await dbContext.Users.FromSqlRaw("SELECT * FROM Users WHERE Login=@username", _username).ToListAsync();
            return user.Count() > 0 ? user.First() : null;
        }
        public async Task<User> GetSingleByMail(string email)
        {
            SqlParameter _email = new SqlParameter("@email", email);
            var user = await dbContext.Users.FromSqlRaw("SELECT * FROM Users WHERE Email=@email", _email).ToListAsync();
            return user.Count() > 0 ? user.First() : null;
        }
        public async Task Update(User entity)
        {
            entity.Password = CoffeeMapServer.Encryptions.Sha1Hash.GetHash(entity.Password);
            SqlParameter _id = new SqlParameter("@id", entity.Id);
            SqlParameter _login = new SqlParameter("@nickname", entity.Login);
            SqlParameter _email = new SqlParameter("@email", entity.Email);
            SqlParameter _password = new SqlParameter("@passwordHash", entity.Password);

            await dbContext.Database.ExecuteSqlRawAsync("UPDATE Users SET Login=@nickname, Email=@email, Password=@passwordHash WHERE Id=@id", _login, _email, _password, _id);
            await dbContext.SaveChangesAsync();

        }
    }
}
